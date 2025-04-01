using InvestmentCompany.ViewModels;
using System.Windows;

namespace InvestmentCompany.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            // Проверяем учетные данные
            var loginViewModel = new LoginViewModel();
            var clientId = loginViewModel.ПроверитьУчетныеДанные(login, password);

            if (clientId != Guid.Empty)
            {
                // Получаем роль пользователя
                var роль = loginViewModel.ПолучитьРольПользователя(clientId);

                // Перенаправляем пользователя в соответствующее окно
                switch (роль)
                {
                    case "Клиент":
                        var clientDetailsViewModel = new ClientDetailsViewModel(clientId);
                        var clientDetailsView = new ClientDetailsView(clientDetailsViewModel);
                        clientDetailsView.Show();
                        break;

                    case "Аналитик":
                        var analystViewModel = new AnalystViewModel();
                        var analystView = new AnalystView(analystViewModel);
                        analystView.Show();
                        break;

                    case "Администратор":
                        var clientViewModel = new ClientViewModel(); // Используем ClientViewModel
                        var clientsView = new ClientsView(clientViewModel); // Передаем ClientViewModel
                        clientsView.Show();
                        break;

                    default:
                        MessageBox.Show("Роль не определена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }

                // Закрываем окно авторизации
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}