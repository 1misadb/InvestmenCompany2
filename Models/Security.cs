using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Guid ID_Эмитента { get; set; }


        public virtual ICollection<SecuritySecurityType> ТипыЦенныхБумаг { get; set; } = new List<SecuritySecurityType>();
        public virtual ICollection<PortfolioAsset> АктивыПортфеля { get; set; }
        [NotMapped] // Указывает EF не маппить это свойство в БД
        public decimal ТекущаяКотировка
        {
            get
            {
                // Логика получения последней котировки из истории
                if (ИсторияКотировок != null && ИсторияКотировок.Any())
                {
                    return ИсторияКотировок.OrderByDescending(h => h.Дата).First().Котировка;
                }
                return 0;
            }
            set { /* можно оставить пустым */ }
        }

        // Добавьте навигационное свойство для истории котировок
        public virtual ICollection<QuoteHistory> ИсторияКотировок { get; set; } = new List<QuoteHistory>();
    }
}