using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmenCompany.Models
{
    public class ClientInvestorType
    {
        [Key]
        public Guid ID_КлиентТипИнвестора { get; set; } = Guid.NewGuid();
        public Guid ID_Клиента { get; set; }
        public Guid ID_ТипаИнвестора { get; set; }
    }
}