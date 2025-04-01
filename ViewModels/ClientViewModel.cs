using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvestmenCompany.Models;
using InvestmenCompany.Services;
using InvestmentCompany.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace InvestmentCompany.ViewModels
{
    public partial class ClientViewModel : ObservableObject
    {
        private readonly InvestmentDbContext _context;

        [ObservableProperty]
        private ObservableCollection<Client> клиенты;

        [ObservableProperty]
        private Client выбранныйКлиент;

        public ClientViewModel()
        {
            _context = new InvestmentDbContext();
            Клиенты = new ObservableCollection<Client>();
            ЗагрузитьКлиентов();
        }

        private void ЗагрузитьКлиентов()
        {
            try
            {
                Клиенты = new ObservableCollection<Client>(_context.Клиенты.AsNoTracking().ToList());
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при загрузке клиентов: {ex.Message}");
                // Дополнительная обработка ошибки, если необходимо
            }
        }

        [RelayCommand]
        private void ОткрытьОкноДобавления()
        {
            var addClientView = new AddClientView(this);
            addClientView.ShowDialog();
        }

        public void ДобавитьКлиента(Client новыйКлиент)
        {
            _context.Клиенты.Add(новыйКлиент);
            _context.SaveChanges();
            Клиенты.Add(новыйКлиент);
        }

        [RelayCommand]
        private void УдалитьКлиента()
        {
            if (ВыбранныйКлиент != null)
            {
                _context.Клиенты.Remove(ВыбранныйКлиент);
                _context.SaveChanges();
                Клиенты.Remove(ВыбранныйКлиент);
            }
        }
        [ObservableProperty]
        private string поисковыйЗапрос;

        [RelayCommand]
        private void ПоискКлиентов()
        {
            if (string.IsNullOrEmpty(ПоисковыйЗапрос))
            {
                Клиенты = new ObservableCollection<Client>(_context.Клиенты.Where(c => c.Название != null && c.Название.Contains(ПоисковыйЗапрос)).ToList());

            }
            else
            {
                Клиенты = new ObservableCollection<Client>(
                    _context.Клиенты.Where(c => c.Название.Contains(ПоисковыйЗапрос)).ToList());
            }
        }
    }
}
