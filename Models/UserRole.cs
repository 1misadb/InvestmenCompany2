using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class UserRole
    {
        [Key]
        public Guid ID_ПользовательРоль { get; set; } = Guid.NewGuid(); // Уникальный идентификатор связи

        public Guid ID_Клиента { get; set; } // Внешний ключ на таблицу Клиент
        public Guid ID_Роли { get; set; } // Внешний ключ на таблицу Роли

        // Навигационные свойства для связи с таблицами Клиент и Роли
        public Client Клиент { get; set; } // Связь с клиентом
        public Role Роль { get; set; } // Связь с ролью
    }
}