using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class PortfolioType
    {
        [Key]
        public Guid ID_ТипаПортфеля { get; set; } = Guid.NewGuid();
        public string Тип { get; set; }
    }
}