using InvestmenCompany.Models;
using InvestmenCompany.Services;
using System.Linq;
using System.Windows;

namespace InvestmentCompany.Views
{
    public partial class RegisterView : Window
    {
        private readonly InvestmentDbContext _context;

        public RegisterView()
        {
            InitializeComponent();
            _context = new InvestmentDbContext();

            // Загрузка типов инвесторов и ролей в ComboBox
            cmbInvestorType.ItemsSource = _context.ТипыИнвесторов.ToList();
            cmbRole.ItemsSource = _context.Роли.ToList();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;
            string name = txtName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;
            var selectedInvestorType = cmbInvestorType.SelectedItem as InvestorType;
            var selectedRole = cmbRole.SelectedItem as Role;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || selectedInvestorType == null || selectedRole == null)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка уникальности логина
            if (_context.Клиенты.Any(c => c.Логин == login))
            {
                MessageBox.Show("Логин уже занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Создание нового клиента
            var newClient = new Client
            {
                ID_Клиента = Guid.NewGuid(),
                Название = name,
                Адрес = address,
                Телефон = phone,
                Логин = login,
                Пароль = password
            };

            // Сохранение клиента в базу данных
            _context.Клиенты.Add(newClient);

            // Связь клиента с типом инвестора
            var clientInvestorType = new ClientInvestorType
            {
                ID_КлиентТипИнвестора = Guid.NewGuid(),
                ID_Клиента = newClient.ID_Клиента,
                ID_ТипаИнвестора = selectedInvestorType.ID_ТипаИнвестора
            };
            _context.КлиентТипИнвестора.Add(clientInvestorType);

            // Связь клиента с ролью
            var userRole = new UserRole
            {
                ID_ПользовательРоль = Guid.NewGuid(),
                ID_Клиента = newClient.ID_Клиента,
                ID_Роли = selectedRole.ID_Роли
            };
            _context.ПользовательРоль.Add(userRole);

            // Сохранение изменений в базе данных
            _context.SaveChanges();

            MessageBox.Show("Регистрация успешна", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}