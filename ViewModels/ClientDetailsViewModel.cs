using InvestmenCompany.Models;
using InvestmenCompany.Services;
using LiveCharts.Wpf;
using LiveCharts;
using Microsoft.EntityFrameworkCore;


namespace InvestmentCompany.ViewModels
{
    public class ClientDetailsViewModel : BaseViewModel
    {
        private Client _текущийКлиент;
        public Client ТекущийКлиент
        {
            get => _текущийКлиент;
            set
            {
                _текущийКлиент = value;
                OnPropertyChanged();
                ОбновитьДиаграммы();
            }
        }

        public SeriesCollection ДоходностьПортфелей { get; set; }
        public List<string> НазванияПортфелей { get; set; }
        public SeriesCollection РаспределениеАктивов { get; set; }
        public decimal ОбщаяСтоимостьПортфеля { get; set; }
        public decimal ВложенныеСредства { get; set; }
        public decimal Прибыль { get; set; }
        public decimal Доходность { get; set; }
        public decimal ПассивныйДоход { get; set; }

        public ClientDetailsViewModel(Guid clientId)
        {
            ДоходностьПортфелей = new SeriesCollection();
            РаспределениеАктивов = new SeriesCollection();
            НазванияПортфелей = new List<string>();

            ЗагрузитьДанныеКлиента(clientId);
        }
        public ICollection<PortfolioAsset> АктивыПортфеля
        {
            get => ТекущийКлиент?.Портфели?.FirstOrDefault()?.АктивыПортфеля;
        }
        private async void ЗагрузитьДанныеКлиента(Guid clientId)
        {
            using (var context = new InvestmentDbContext())
            {
                ТекущийКлиент = await context.Клиенты
                    .Include(c => c.Портфели)
                        .ThenInclude(p => p.АктивыПортфеля)
                            .ThenInclude(a => a.ЦеннаяБумага)
                            .ThenInclude(s => s.ИсторияКотировок)
                    .Include(c => c.Портфели)
                        .ThenInclude(p => p.ТипыПортфелей)
                            .ThenInclude(pt => pt.ТипПортфеля)
                    .FirstOrDefaultAsync(c => c.ID_Клиента == clientId);

                РассчитатьОбщиеПоказатели(); 
                ОбновитьДиаграммы();
            }
        }
        private void РассчитатьОбщиеПоказатели()
        {
            if (ТекущийКлиент?.Портфели == null || !ТекущийКлиент.Портфели.Any())
                return;

            using (var context = new InvestmentDbContext())
            {
                foreach (var портфель in ТекущийКлиент.Портфели)
                {
                    foreach (var актив in портфель.АктивыПортфеля)
                    {
                        var последняяКотировка = context.ИсторияКотировок
                            .Where(h => h.ID_ЦеннойБумаги == актив.ID_ЦеннойБумаги)
                            .OrderByDescending(h => h.Дата)
                            .FirstOrDefault();

                        актив.ЦеннаяБумага.ТекущаяКотировка = последняяКотировка?.Котировка ?? 0;
                    }
                }

                // Правильный расчет стоимости портфеля
                ОбщаяСтоимостьПортфеля = Math.Round(ТекущийКлиент.Портфели
                    .Sum(p => p.АктивыПортфеля?.Sum(a =>
                        (a.ДоляАктива / 100m) * (a.ЦеннаяБумага?.ТекущаяКотировка ?? 0m)) ?? 0m), 4);

                ВложенныеСредства = Math.Round(ТекущийКлиент.Портфели
                    .Sum(p => p.АктивыПортфеля?.Sum(a =>
                        (a.ДоляАктива / 100m) * (a.ЦеннаяБумага?.МинимальнаяСуммаСделки ?? 0m)) ?? 0m), 4);

                Прибыль = ОбщаяСтоимостьПортфеля - ВложенныеСредства;
                Доходность = (ВложенныеСредства != 0) ? (Прибыль / ВложенныеСредства) * 100 : 0;
                ПассивныйДоход = ТекущийКлиент.Портфели
                    .Sum(p => p.АктивыПортфеля?.Sum(a => a.ДоходностьАктива * (a.ДоляАктива / 100m)) ?? 0);
                Console.WriteLine($"Общая стоимость: {ОбщаяСтоимостьПортфеля}");
                Console.WriteLine($"Вложенные средства: {ВложенныеСредства}");
                foreach (var актив in ТекущийКлиент.Портфели.First().АктивыПортфеля)
                {
                    Console.WriteLine($"Актив: {актив.ЦеннаяБумага?.Название}, Доля: {актив.ДоляАктива}, Котировка: {актив.ЦеннаяБумага?.ТекущаяКотировка}");
                }
            }

            OnPropertyChanged(nameof(ОбщаяСтоимостьПортфеля));
            OnPropertyChanged(nameof(ВложенныеСредства));
            OnPropertyChanged(nameof(Прибыль));
            OnPropertyChanged(nameof(Доходность));
            OnPropertyChanged(nameof(ПассивныйДоход));
        }

        private void ОбновитьДиаграммы()
        {
            if (ТекущийКлиент == null || ТекущийКлиент.Портфели == null) return;

            ДоходностьПортфелей.Clear();
            РаспределениеАктивов.Clear();
            НазванияПортфелей.Clear();

            // Доходность портфелей
            foreach (var портфель in ТекущийКлиент.Портфели)
            {
                var доходность = портфель.АктивыПортфеля?.Sum(актив =>
                    актив.ДоходностьАктива * (актив.ДоляАктива / 100)) ?? 0;

                ДоходностьПортфелей.Add(new ColumnSeries
                {
                    Title = портфель.НазваниеПортфеля,
                    Values = new ChartValues<decimal> { доходность }
                });

                НазванияПортфелей.Add(портфель.НазваниеПортфеля);
            }

            // Распределение активов (для первого портфеля)
            var первыйПортфель = ТекущийКлиент.Портфели.FirstOrDefault();
            if (первыйПортфель?.АктивыПортфеля != null)
            {
                foreach (var актив in первыйПортфель.АктивыПортфеля)
                {
                    РаспределениеАктивов.Add(new PieSeries
                    {
                        Title = актив.ЦеннаяБумага?.Название ?? "Неизвестно",
                        Values = new ChartValues<decimal> { актив.ДоляАктива },
                        DataLabels = true
                    });
                }
            }
        }
    }
}