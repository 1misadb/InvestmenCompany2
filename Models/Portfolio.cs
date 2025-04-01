using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class Portfolio
    {
        [Key]
        public Guid ID_Портфеля { get; set; } = Guid.NewGuid();

        public Guid ID_Клиента { get; set; } // Внешний ключ на Клиента

        public string НазваниеПортфеля { get; set; }
        public DateTime ДатаСоздания { get; set; }

        // Навигационное свойство для клиента
        public Client Клиент { get; set; }

        // Навигационное свойство для активов портфеля
        public ICollection<PortfolioAsset> АктивыПортфеля { get; set; } = new List<PortfolioAsset>();
    }
}