using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class PortfolioAsset
    {
        [Key]
        public Guid ID_АктиваПортфеля { get; set; } = Guid.NewGuid();
        public Guid ID_Портфеля { get; set; } // Внешний ключ на Портфель
        public Guid ID_ЦеннойБумаги { get; set; } // Внешний ключ на ЦеннуюБумагу
        public decimal ДоляАктива { get; set; }
        public decimal ДоходностьАктива { get; set; }

        // Навигационное свойство для портфеля
        public Portfolio Портфель { get; set; }

        // Навигационное свойство для ценной бумаги
        public Security ЦеннаяБумага { get; set; }
    }
}