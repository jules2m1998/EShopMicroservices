using Marten.Schema;

namespace Catalog.Api.Data;

internal static class ProductSeedData
{
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Wireless Mouse",
                Categories = ["Electronics", "Accessories", "Office"],
                Description = "Ergonomic wireless mouse with adjustable DPI.",
                ImageFile = "wireless_mouse.jpg",
                Price = 29.99m,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Gaming Keyboard",
                Categories = ["Electronics", "Accessories", "Gaming"],
                Description = "RGB mechanical keyboard with programmable keys.",
                ImageFile = "gaming_keyboard.jpg",
                Price = 79.99m,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone Stand",
                Categories = ["Electronics", "Accessories"],
                Description = "Adjustable smartphone stand for hands-free usage.",
                ImageFile = "smartphone_stand.jpg",
                Price = 14.99m,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Bluetooth Speaker",
                Categories = ["Electronics", "Audio"],
                Description = "Portable Bluetooth speaker with 10-hour battery life.",
                ImageFile = "bluetooth_speaker.jpg",
                Price = 49.99m,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "4K Monitor",
                Categories = ["Electronics", "Displays", "Office"],
                Description = "27-inch 4K monitor with HDR support and ultra-thin bezels.",
                ImageFile = "4k_monitor.jpg",
                Price = 299.99m,
            },
        };
    }
}

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        session.Store<Product>(ProductSeedData.GetProducts());
        await session.SaveChangesAsync(cancellation);
    }
}
