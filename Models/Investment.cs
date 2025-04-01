using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class Investment
    {
        [Key]
        public Guid ID_Инвестиции { get; set; } = Guid.NewGuid();
        public Guid ID_Клиента { get; set; } // Внешний ключ на Клиента
        public Guid ID_ЦеннойБумаги { get; set; } // Внешний ключ на ЦеннуюБумагу
        public decimal Котировка { get; set; }
        public DateTime ДатаПокупки { get; set; }
        public DateTime? ДатаПродажи { get; set; } // Может быть null, если продажа еще не произошла

        // Навигационное свойство для клиента
        public Client Клиент { get; set; }

        // Навигационное свойство для ценной бумаги
        public Security ЦеннаяБумага { get; set; }
    }
}