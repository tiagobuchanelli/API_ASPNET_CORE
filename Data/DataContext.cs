using Microsoft.EntityFrameworkCore;
using Lojax.Models;

//Representação do DB em memória
namespace Lojax.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Representação das tabelas em memoria
        public DbSet<Category> Categories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<FinanceApAr> FinanceApArs { get; set; }
        public DbSet<FinanceInstallment> FinanceInstallments { get; set; }
        public DbSet<OperationFinance> OperationFinance { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMov> StockMovs { get; set; }
        public DbSet<User> Users { get; set; }

    }
}