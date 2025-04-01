using InvestmenCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmenCompany.Services
{
    public class InvestmentDbContext : DbContext
    {
        public DbSet<Client> Клиенты { get; set; }
        public DbSet<InvestorType> ТипыИнвесторов { get; set; }
        public DbSet<ClientInvestorType> КлиентТипИнвестора { get; set; }
        public DbSet<Issuer> Эмитенты { get; set; }
        public DbSet<IssuerType> ТипыЭмитентов { get; set; }
        public DbSet<IssuerIssuerType> ЭмитентТипЭмитента { get; set; }
        public DbSet<Security> ЦенныеБумаги { get; set; }
        public DbSet<SecurityType> ТипыЦенныхБумаг { get; set; }
        public DbSet<SecuritySecurityType> ЦеннаяБумагаТипЦеннойБумаги { get; set; }
        public DbSet<Investment> Инвестиции { get; set; }
        public DbSet<QuoteHistory> ИсторияКотировок { get; set; }
        public DbSet<Portfolio> Портфели { get; set; }
        public DbSet<PortfolioType> ТипыПортфелей { get; set; }
        public DbSet<PortfolioPortfolioType> ПортфельТипПортфеля { get; set; }
        public DbSet<PortfolioAsset> АктивыПортфеля { get; set; }
        public DbSet<UserRole> ПользовательРоль { get; set; }
        public DbSet<Role> Роли { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Server=1misa;Database=InvestmentCompany;Trusted_Connection=true;TrustServerCertificate=true;encrypt=false";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Указываем правильные имена таблиц
            modelBuilder.Entity<Client>().ToTable("Клиент");
            modelBuilder.Entity<InvestorType>().ToTable("ТипИнвестора");
            modelBuilder.Entity<ClientInvestorType>().ToTable("КлиентТипИнвестора");
            modelBuilder.Entity<Issuer>().ToTable("Эмитент");
            modelBuilder.Entity<IssuerType>().ToTable("ТипЭмитента");
            modelBuilder.Entity<IssuerIssuerType>().ToTable("ЭмитентТипЭмитента");
            modelBuilder.Entity<Security>().ToTable("ЦеннаяБумага");
            modelBuilder.Entity<SecurityType>().ToTable("ТипЦеннойБумаги");
            modelBuilder.Entity<SecuritySecurityType>().ToTable("ЦеннаяБумагаТипЦеннойБумаги");
            modelBuilder.Entity<Investment>().ToTable("Инвестиция");
            modelBuilder.Entity<QuoteHistory>().ToTable("ИсторияКотировок");
            modelBuilder.Entity<Portfolio>().ToTable("Портфель");
            modelBuilder.Entity<PortfolioType>().ToTable("ТипПортфеля");
            modelBuilder.Entity<PortfolioPortfolioType>().ToTable("ПортфельТипПортфеля");
            modelBuilder.Entity<PortfolioAsset>().ToTable("АктивыПортфеля");

            // Настройка связей и ключей
            modelBuilder.Entity<ClientInvestorType>()
                .HasKey(cti => new { cti.ID_Клиента, cti.ID_ТипаИнвестора });

            modelBuilder.Entity<IssuerIssuerType>()
                .HasKey(ete => new { ete.ID_Эмитента, ete.ID_ТипаЭмитента });

            modelBuilder.Entity<SecuritySecurityType>()
                .HasKey(cbt => new { cbt.ID_ЦеннойБумаги, cbt.ID_ТипаЦеннойБумаги });

            modelBuilder.Entity<PortfolioPortfolioType>()
                .HasKey(ptp => new { ptp.ID_Портфеля, ptp.ID_ТипаПортфеля });

            // Настройка связей для UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.ID_ПользовательРоль);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Клиент)
                .WithMany(c => c.ПользовательРоль)
                .HasForeignKey(ur => ur.ID_Клиента);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Роль)
                .WithMany(r => r.ПользовательРоль)
                .HasForeignKey(ur => ur.ID_Роли);

            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.Клиент) 
                .WithMany(c => c.Портфели) 
                .HasForeignKey(p => p.ID_Клиента) 
                .HasConstraintName("FK_Портфель_Клиент"); // Указываем имя внешнего ключа

            modelBuilder.Entity<PortfolioAsset>()
                .HasOne(pa => pa.Портфель)
                .WithMany(p => p.АктивыПортфеля)
                .HasForeignKey(pa => pa.ID_Портфеля)
                .HasConstraintName("FK_АктивыПортфеля_Портфель"); // Указываем имя внешнего ключа

            modelBuilder.Entity<PortfolioAsset>()
                .HasOne(pa => pa.ЦеннаяБумага)
                .WithMany()
                .HasForeignKey(pa => pa.ID_ЦеннойБумаги)
                .HasConstraintName("FK_АктивыПортфеля_ЦеннаяБумага"); // Указываем имя внешнего ключа
        }
    }
}