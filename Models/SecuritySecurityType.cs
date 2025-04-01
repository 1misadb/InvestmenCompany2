using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class SecuritySecurityType
    {
        [Key]
        public Guid ID_ЦеннаяБумагаТипЦеннойБумаги { get; set; } = Guid.NewGuid();
        public Guid ID_ЦеннойБумаги { get; set; }
        public Guid ID_ТипаЦеннойБумаги { get; set; }

        // Навигационные свойства
        public Security ЦеннаяБумага { get; set; }
        public SecurityType ТипЦеннойБумаги { get; set; }
    }
}