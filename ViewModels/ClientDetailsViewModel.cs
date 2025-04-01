using InvestmenCompany.Models;
using InvestmenCompany.Services;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;


namespace InvestmentCompany.ViewModels
{
    public class ClientDetailsViewModel : BaseViewModel
    {
        private Client _текущийКлиент;
        public Client ТекущийКлиент { get; set; }

        public ClientDetailsViewModel(Guid clientId)
        {
            using (var context = new InvestmentDbContext())
            {
                ТекущийКлиент = context.Клиенты
                    .Include(c => c.Портфели)
                    .ThenInclude(p => p.АктивыПортфеля)
                    .ThenInclude(a => a.ЦеннаяБумага)
                    .FirstOrDefault(c => c.ID_Клиента == clientId);
            }
        }

        // Данные для диаграммы доходности портфелей
        public SeriesCollection ДоходностьПортфелей { get; set; }
        public List<string> НазванияПортфелей { get; set; }

        // Данные для диаграммы распределения активов
        public SeriesCollection РаспределениеАктивов { get; set; }

        public ClientDetailsViewModel(Client клиент)
        {
            ТекущийКлиент = клиент;
            ДоходностьПортфелей = new SeriesCollection();
            РаспределениеАктивов = new SeriesCollection();
            НазванияПортфелей = new List<string>();
        }

        private void ОбновитьДиаграммы()
        {
            // Очистка старых данных
            ДоходностьПортфелей.Clear();
            РаспределениеАктивов.Clear();
            НазванияПортфелей.Clear();

            // Заполнение данных для диаграммы доходности портфелей
            foreach (var портфель in ТекущийКлиент.Портфели)
            {
                var доходность = портфель.АктивыПортфеля.Sum(актив => актив.ДоходностьАктива * актив.ДоляАктива / 100);
                ДоходностьПортфелей.Add(new ColumnSeries
                {
                    Title = портфель.НазваниеПортфеля,
                    Values = new ChartValues<decimal> { доходность }
                });
                НазванияПортфелей.Add(портфель.НазваниеПортфеля);
            }

            // Заполнение данных для диаграммы распределения активов
            foreach (var портфель in ТекущийКлиент.Портфели)
            {
                foreach (var актив in портфель.АктивыПортфеля)
                {
                    РаспределениеАктивов.Add(new PieSeries
                    {
                        Title = актив.ЦеннаяБумага.Название,
                        Values = new ChartValues<decimal> { актив.ДоляАктива },
                        DataLabels = true
                    });
                }
            }
        }
    }
}