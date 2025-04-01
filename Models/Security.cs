using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class Security
    {
        [Key]
        public Guid ID_ЦеннойБумаги { get; set; } = Guid.NewGuid();
        public string Название { get; set; }
        public string Рейтинг { get; set; }
        public decimal ДоходностьЗаПрошлыйГод { get; set; }
        public decimal МинимальнаяСуммаСделки { get; set; }
        public Guid ID_Эмитента { get; set; } // Внешний ключ на Эмитента

        // Навигационное свойство для связи с SecuritySecurityType
        public ICollection<SecuritySecurityType> SecuritySecurityTypes { get; set; } = new List<SecuritySecurityType>();
    }
}