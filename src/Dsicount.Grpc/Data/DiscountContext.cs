using Dsicount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Dsicount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<Coupon>()
            .HasData(
                new Coupon
                {
                    Id = 1,
                    ProductName = "Wireless Mouse",
                    Description =
                        "Get a discount on the ergonomic wireless mouse with adjustable DPI.",
                    Amount =
                        5 // Assuming a $5 discount
                    ,
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "Gaming Keyboard",
                    Description =
                        "Save on the RGB mechanical gaming keyboard with programmable keys.",
                    Amount =
                        10 // Assuming a $10 discount
                    ,
                }
            );
    }
}
