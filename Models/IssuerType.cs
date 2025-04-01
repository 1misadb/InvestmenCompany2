using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class IssuerType
    {
        [Key]
        public Guid ID_ТипаЭмитента { get; set; } = Guid.NewGuid();
        public string Тип { get; set; }
    }
}