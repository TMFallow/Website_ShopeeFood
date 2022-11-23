using Data;
using Microsoft.EntityFrameworkCore;
using ShopeeFood_Data.Model;

namespace ShopeeFood_Repository
{
    public class Application_Context : DbContext
    {

        public Application_Context()
        {

        }

        public Application_Context(DbContextOptions<Application_Context> options) : base(options)
        {

        }

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InvoiceDetails>().HasKey(s => new { s.InvoicesID, s.FoodId });
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Application_Context).Assembly);
        }

        public DbSet<Restaurant> restaurants { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Foods> foods { get; set; }

        public DbSet<Types> types { get; set; }

        public DbSet<Areas> areas { get; set; }

        public DbSet<AddressToDelivery> addresses { get; set; }

        public DbSet<Invoices> invoices { get; set; }

        public DbSet<InvoiceDetails> invoiceDetails { get; set; }

        public DbSet<Promotion> promotions { get; set; }

        public DbSet<DetailAreas> detailAreas { get; set; }

        public DbSet<Token> tokens { get; set; }
    }
}