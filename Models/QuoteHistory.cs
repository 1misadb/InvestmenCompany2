using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class QuoteHistory
    {
        [Key]
        public Guid ID_ИсторииКотировок { get; set; } = Guid.NewGuid();
        public Guid ID_ЦеннойБумаги { get; set; } // Внешний ключ на ЦеннуюБумагу
        public decimal Котировка { get; set; }
        public DateTime Дата { get; set; }
    }
}