using InvestmenCompany.Services;
using System;
using System.Linq;

namespace InvestmentCompany.ViewModels
{
    public class LoginViewModel
    {
        public Guid ПроверитьУчетныеДанные(string login, string password)
        {
            using (var context = new InvestmentDbContext())
            {
                var клиент = context.Клиенты
                    .FirstOrDefault(c => c.Логин == login && c.Пароль == password);

                return клиент?.ID_Клиента ?? Guid.Empty;
            }
        }

        public string ПолучитьРольПользователя(Guid clientId)
        {
            using (var context = new InvestmentDbContext())
            {
                var роль = context.ПользовательРоль
                    .Where(ur => ur.ID_Клиента == clientId)
                    .Join(context.Роли,
                          ur => ur.ID_Роли,
                          r => r.ID_Роли,
                          (ur, r) => r.Название)
                    .FirstOrDefault();

                return роль;
            }
        }
    }
}