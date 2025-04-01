using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class Issuer
    {
        [Key]
        public Guid ID_Эмитента { get; set; } = Guid.NewGuid();
        public string Название { get; set; }
    }
}