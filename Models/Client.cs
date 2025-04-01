using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class Client
    {
        [Key]
        public Guid ID_Клиента { get; set; } = Guid.NewGuid();
        public string Название { get; set; }
        public string Адрес { get; set; }
        public string Телефон { get; set; }
        public string Логин { get; set; }
        public string Пароль { get; set; }

        // Навигационное свойство для портфелей клиента
        public ICollection<Portfolio> Портфели { get; set; } = new List<Portfolio>();
        public ICollection<ClientInvestorType> КлиентТипИнвестора { get; set; } = new List<ClientInvestorType>();
        public ICollection<UserRole> ПользовательРоль { get; set; } = new List<UserRole>();
    }
}