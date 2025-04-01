using InvestmentCompany.ViewModels;
using System.Windows;

namespace InvestmentCompany.Views
{
    public partial class ClientsView : Window
    {
        public ClientsView()
        {
            InitializeComponent();
            DataContext = new ClientViewModel(); // Используется, если ViewModel не передается извне
        }

        // Добавляем конструктор, принимающий ClientViewModel
        public ClientsView(ClientViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; // Устанавливаем переданную ViewModel в качестве DataContext
        }
    }
}