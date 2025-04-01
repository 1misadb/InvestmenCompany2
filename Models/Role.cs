using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class Role
    {
        [Key]
        public Guid ID_Роли { get; set; } = Guid.NewGuid(); // Уникальный идентификатор роли

        public string Название { get; set; } // Название роли (например, "Клиент", "Аналитик", "Администратор")

        // Навигационное свойство для связи с таблицей ПользовательРоль
        public ICollection<UserRole> ПользовательРоль { get; set; } = new List<UserRole>();
    }
}