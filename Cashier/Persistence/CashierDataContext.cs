using Cashier.Model;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Cashier.Persistence
{
    public class CashierDataContext : DbContext
    {
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Saldo>   Saldo    { get; set; }

        public CashierDataContext() : base(nameOrConnectionString:"Default")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
