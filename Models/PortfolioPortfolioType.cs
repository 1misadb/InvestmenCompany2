using InvestmenCompany.Models;
using System.ComponentModel.DataAnnotations;

public class PortfolioPortfolioType
{
    [Key]
    public Guid ID_ПортфельТипПортфеля { get; set; } = Guid.NewGuid();
    public Guid ID_Портфеля { get; set; }
    public Guid ID_ТипаПортфеля { get; set; }

    // Навигационные свойства
    public Portfolio Портфель { get; set; }
    public PortfolioType ТипПортфеля { get; set; }
}