using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class SecurityType
    {
        [Key]
        public Guid ID_ТипаЦеннойБумаги { get; set; } = Guid.NewGuid();
        public string Тип { get; set; }
    }
}