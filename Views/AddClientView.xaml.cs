using InvestmenCompany.Models;
using InvestmentCompany.ViewModels;
using System.Windows;

namespace InvestmentCompany.Views
{
    public partial class AddClientView : Window
    {
        private ClientViewModel _viewModel;

        public AddClientView(ClientViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем нового клиента с данными из текстовых полей
            var newClient = new Client
            {
                Название = txtName.Text,
                Адрес = txtAddress.Text,
                Телефон = txtPhone.Text
            };

            // Передаем нового клиента в ViewModel для добавления
            _viewModel.ДобавитьКлиента(newClient);

            // Закрываем окно
            this.Close();
        }
    }
}