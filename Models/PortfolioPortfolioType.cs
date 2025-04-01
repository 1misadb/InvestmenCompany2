using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class PortfolioPortfolioType
    {
        [Key]
        public Guid ID_ПортфельТипПортфеля { get; set; } = Guid.NewGuid();
        public Guid ID_Портфеля { get; set; }
        public Guid ID_ТипаПортфеля { get; set; }
    }
}