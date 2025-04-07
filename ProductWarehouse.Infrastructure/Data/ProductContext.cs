using Microsoft.EntityFrameworkCore;
using ProductWarehouse.Enitity.Entities;

namespace ProductWarehouse.Infrastructure.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        { }
        public DbSet<Products> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence<int>("ProductIdSequence")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.ProductId)
                .HasDefaultValueSql("NEXT VALUE FOR ProductIdSequence");

                entity.HasIndex(e => e.ProductId)
                .IsUnique();

                entity.ToTable(e =>
                {
                    e.HasCheckConstraint("CK_Products_TotalQuantity_NonNegative", "[TotalQuantity] >= 0");
                    e.HasCheckConstraint("CK_Products_Price_NonNegative", "[Price] > 0");
                });
            });
        }
    }
}
