using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class IssuerIssuerType
    {
        [Key]
        public Guid ID_ЭмитентТипЭмитента { get; set; } = Guid.NewGuid();
        public Guid ID_Эмитента { get; set; }
        public Guid ID_ТипаЭмитента { get; set; }
    }
}