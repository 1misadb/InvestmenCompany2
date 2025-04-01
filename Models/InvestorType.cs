using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class InvestorType
    {
        [Key]
        public Guid ID_ТипаИнвестора { get; set; } = Guid.NewGuid();
        public string Тип { get; set; }
    }
}