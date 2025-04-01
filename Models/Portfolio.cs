using InvestmenCompany.Models;
using System.ComponentModel.DataAnnotations;

public class Portfolio
{
    [Key]
    public Guid ID_Портфеля { get; set; } = Guid.NewGuid();

    public Guid ID_Клиента { get; set; } // Внешний ключ на Клиента
    public string НазваниеПортфеля { get; set; }
    public DateTime ДатаСоздания { get; set; }

    // Навигационные свойства
    public Client Клиент { get; set; }
    public ICollection<PortfolioAsset> АктивыПортфеля { get; set; } = new List<PortfolioAsset>();

    // Добавьте связь с типами портфелей (многие-ко-многим)
    public ICollection<PortfolioPortfolioType> ТипыПортфелей { get; set; } = new List<PortfolioPortfolioType>();
}