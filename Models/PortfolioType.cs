using System.ComponentModel.DataAnnotations;

public class PortfolioType
{
    [Key]
    public Guid ID_ТипаПортфеля { get; set; } = Guid.NewGuid();
    public string Тип { get; set; }

    // Коллекция для связи многие-ко-многим
    public ICollection<PortfolioPortfolioType> Портфели { get; set; } = new List<PortfolioPortfolioType>();
}