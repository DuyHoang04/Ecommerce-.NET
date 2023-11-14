using Ecommerce.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(b => b.Email).IsUnique();



            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(b => b.Name).IsUnique();

                entity.HasMany(b => b.ProductList)
                    .WithOne(b => b.Brand)
                    .HasForeignKey(p => p.BrandId)
                    .IsRequired(false);

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                // manty to many with category
                entity.HasMany(c => c.ProductList)
                    .WithMany(p => p.Categories);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(pm => pm.PaymentMethod).IsUnique();
                entity.Property(pm => pm.Amount).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
                // one to many with image
                entity.HasMany(p => p.Images)
                    .WithOne(i => i.Product)
                    .HasForeignKey(i => i.ProductId);

                entity.Property(p => p.Price).HasColumnType("decimal(10, 2)");
                // one to many with rating
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(ot => ot.TotalPrice).HasColumnType("decimal(10, 2)");
                // one to many with promotion
              

                entity.HasOne(ot => ot.Product)
                    .WithMany()
                    .HasForeignKey(ot => ot.ProductId)
                    .IsRequired(false);

                entity.HasOne(ot => ot.User)
                  .WithMany(u => u.OrderItems)
                  .HasForeignKey(ot => ot.UserId)
                  .IsRequired(false);

            });

            modelBuilder.Entity<Rating>(entity =>
            {

                entity.HasOne(r => r.Product)
                       .WithMany(p => p.Ratings)
                       .HasForeignKey(r => r.ProductId);

                entity.HasOne(r => r.User)
                      .WithMany(p => p.Ratings)
                      .HasForeignKey(r => r.UserId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(o => o.Code).IsUnique();
                entity.Property(p => p.TotalAmount).HasColumnType("decimal(10, 2)");

                // many to one with orderItem
                entity.HasMany(o => o.OrderItemList)
                      .WithOne(ot => ot.Order)
                      .HasForeignKey(ot => ot.OrderId)
                      .IsRequired(false);

                // one to many with payment

                entity.HasOne(o => o.Payment)
                       .WithMany()
                       .HasForeignKey(o => o.PaymentId)
                       .IsRequired(false);

                // one to many with user
                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.UserId)
                      .IsRequired(false);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasIndex(pm => pm.Code).IsUnique();
                entity.Property(p => p.DiscountPercent).HasColumnType("decimal(10, 2)");

            });


        }

    }
}
