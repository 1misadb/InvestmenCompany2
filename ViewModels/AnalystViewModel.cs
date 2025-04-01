using InvestmenCompany.Services;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InvestmentCompany.ViewModels
{
    public class AnalystViewModel : BaseViewModel
    {
        private readonly InvestmentDbContext _context;

        public SeriesCollection ДоходностьПортфелей { get; set; }
        public List<string> НазванияПортфелей { get; set; }

        public SeriesCollection РаспределениеАктивовПоТипам { get; set; }

        public AnalystViewModel()
        {
            _context = new InvestmentDbContext();
            ДоходностьПортфелей = new SeriesCollection();
            НазванияПортфелей = new List<string>();
            РаспределениеАктивовПоТипам = new SeriesCollection();

            ЗагрузитьДанные();
            ОбновитьДиаграммы();
        }

        private void ЗагрузитьДанные()
        {
            var портфели = _context.Портфели
                .Include(p => p.АктивыПортфеля)
                .ThenInclude(a => a.ЦеннаяБумага)
                .ThenInclude(s => s.ТипыЦенныхБумаг)
                .ThenInclude(sst => sst.ТипЦеннойБумаги)
                .ToList();

            // Заполнение данных для диаграмм
            foreach (var портфель in портфели)
            {
                var доходность = портфель.АктивыПортфеля.Sum(актив => актив.ДоходностьАктива * актив.ДоляАктива / 100);
                ДоходностьПортфелей.Add(new ColumnSeries
                {
                    Title = портфель.НазваниеПортфеля,
                    Values = new ChartValues<decimal> { доходность }
                });
                НазванияПортфелей.Add(портфель.НазваниеПортфеля);
            }

            // Заполнение данных для диаграммы распределения активов по типам
            var типыАктивов = портфели
                .SelectMany(p => p.АктивыПортфеля)
                .GroupBy(a => a.ЦеннаяБумага.ТипыЦенныхБумаг.FirstOrDefault()?.ТипЦеннойБумаги.Тип)
                .Where(t => t.Key != null)
                .Select(g => new { Тип = g.Key, Доля = g.Count() });

            foreach (var тип in типыАктивов)
            {
                РаспределениеАктивовПоТипам.Add(new PieSeries
                {
                    Title = тип.Тип,
                    Values = new ChartValues<decimal> { тип.Доля },
                    DataLabels = true
                });
            }
        }

        private void ОбновитьДиаграммы()
        {
            // Очистка старых данных
            ДоходностьПортфелей.Clear();
            РаспределениеАктивовПоТипам.Clear();
            НазванияПортфелей.Clear();

            // Загрузка данных из базы данных
            var портфели = _context.Портфели
                .Include(p => p.АктивыПортфеля)
                .ThenInclude(a => a.ЦеннаяБумага)
                .ThenInclude(s => s.ТипыЦенныхБумаг)
                .ThenInclude(sst => sst.ТипЦеннойБумаги)
                .ToList();

            // Заполнение данных для диаграммы доходности портфелей
            foreach (var портфель in портфели)
            {
                var доходность = портфель.АктивыПортфеля.Sum(актив => актив.ДоходностьАктива * актив.ДоляАктива / 100);
                ДоходностьПортфелей.Add(new ColumnSeries
                {
                    Title = портфель.НазваниеПортфеля,
                    Values = new ChartValues<decimal> { доходность }
                });
                НазванияПортфелей.Add(портфель.НазваниеПортфеля);
            }

            // Заполнение данных для диаграммы распределения активов по типам
            var типыАктивов = портфели
                .SelectMany(p => p.АктивыПортфеля)
                .GroupBy(a => a.ЦеннаяБумага.ТипыЦенныхБумаг.FirstOrDefault()?.ТипЦеннойБумаги.Тип)
                .Where(t => t.Key != null)
                .Select(g => new { Тип = g.Key, Доля = g.Count() });

            foreach (var тип in типыАктивов)
            {
                РаспределениеАктивовПоТипам.Add(new PieSeries
                {
                    Title = тип.Тип,
                    Values = new ChartValues<decimal> { тип.Доля },
                    DataLabels = true
                });
            }
        }
    }
}