using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CentralStore.LocalStore.Domain
{
  public class LocalStoreDbContext : DbContext
  {
    public DbSet<Product> Products { get; set; }

    public LocalStoreDbContext(DbContextOptions<LocalStoreDbContext> options)
      : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Product>(product => 
      {
        product.HasKey(p => p.Id);
        product.Property(p => p.Name).IsRequired().HasMaxLength(100);
        product.Property(p => p.Description).IsRequired().HasMaxLength(500);
        product.Property(p => p.Price).HasPrecision(10, 2);
        product.Property(p => p.MinPrice).HasPrecision(10, 2);

          product.HasData(
            new Product
            {
                Id = Guid.Parse("1a8c30a4-0445-4b1f-b01c-4a5c8f4d832f"),
                Name = "Aurora Conference Table",
                Description = "Large solid maple conference table with integrated cable troughs.",
                Price = 1399.99m,
                MinPrice = 1299.99m,
                CreatedAt = new DateTime(2024, 1, 3, 8, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 4, 9, 15, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("bfe2f7fd-3551-4d13-9fcd-d0c1bebafd00")
            },
            new Product
            {
                Id = Guid.Parse("2f5d93e7-1b60-4687-9baa-c2aa1c5dc65a"),
                Name = "Lumen Lounge Chair",
                Description = "Ergonomic lounge chair upholstered in perforated leather.",
                Price = 649.50m,
                MinPrice = 599.00m,
                CreatedAt = new DateTime(2024, 1, 5, 13, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 5, 16, 45, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("75cbf198-7ffb-4ee2-9b26-17fa16cbd59e")
            },
            new Product
            {
                Id = Guid.Parse("3662d4ac-1a6f-447d-80b0-4673c7c7fc56"),
                Name = "Northwind Standing Desk",
                Description = "Height-adjustable standing desk with waterfall edge and programmable presets.",
                Price = 999.95m,
                MinPrice = 899.95m,
                CreatedAt = new DateTime(2024, 1, 6, 9, 20, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 6, 9, 25, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("8f6e54f6-4ab9-40b3-8592-7c5d0d4d1a2e")
            },
            new Product
            {
                Id = Guid.Parse("4442e177-f6ea-4b77-9f07-4f44d7f1b137"),
                Name = "Solstice Task Lamp",
                Description = "LED task lamp offering tunable white light and touch dimming.",
                Price = 129.99m,
                MinPrice = 109.99m,
                CreatedAt = new DateTime(2024, 1, 7, 7, 10, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 7, 7, 10, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("3d54c37c-61b9-4139-a6ab-149226c95ebb")
            },
            new Product
            {
                Id = Guid.Parse("5bacb48d-43d6-4f06-9cf8-16c1f8be6b66"),
                Name = "Vela Reception Sofa",
                Description = "Modular reception sofa with antimicrobial fabric.",
                Price = 1899.00m,
                MinPrice = 1799.00m,
                CreatedAt = new DateTime(2024, 1, 8, 14, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 9, 10, 0, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("97b3f65a-099e-4a6e-bfd8-1c3e679a5b87")
            },
            new Product
            {
                Id = Guid.Parse("686b45f0-cb33-4d03-b040-6a8d966b0f29"),
                Name = "Cascade Acoustic Panel",
                Description = "Wall-mounted acoustic panel with felt finish.",
                Price = 249.00m,
                MinPrice = 229.00m,
                CreatedAt = new DateTime(2024, 1, 10, 11, 15, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 10, 11, 15, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("a6d4eed6-9e7b-4bfa-92c6-2e63a2dc4d5e")
            },
            new Product
            {
                Id = Guid.Parse("77cc8d8e-bfa1-4f4d-b594-0bb9f8ce7a8a"),
                Name = "Harbor Mobile Pedestal",
                Description = "Steel mobile pedestal with silent casters and lock.",
                Price = 379.25m,
                MinPrice = 349.25m,
                CreatedAt = new DateTime(2024, 1, 12, 15, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 12, 15, 15, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("0c2df9b7-dd68-4e96-8718-ffbb9b5b1a75")
            },
            new Product
            {
                Id = Guid.Parse("8237c4d1-2805-4f65-9c37-18452f2fe14b"),
                Name = "Mercury Executive Chair",
                Description = "High-back executive chair with synchronous tilt and leather.",
                Price = 799.99m,
                MinPrice = 749.99m,
                CreatedAt = new DateTime(2024, 1, 13, 10, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 13, 10, 35, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("bd6c731f-5083-4bed-8f36-54d4efbf8f90")
            },
            new Product
            {
                Id = Guid.Parse("92ea71b4-6f7b-4c76-a0bf-aee9871d2a78"),
                Name = "Zenith Modular Shelf",
                Description = "Modular shelving system with powder-coated frames.",
                Price = 459.75m,
                MinPrice = 429.75m,
                CreatedAt = new DateTime(2024, 1, 14, 9, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 14, 9, 0, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("65e5a1b6-0b25-4de1-b61f-11cad0ff6a47")
            },
            new Product
            {
                Id = Guid.Parse("a240d279-c935-4392-becf-2eceaab4e52d"),
                Name = "Vertex Whiteboard",
                Description = "Magnetic dry-erase board with aluminum frame.",
                Price = 219.99m,
                MinPrice = 199.99m,
                CreatedAt = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("f6ef3101-2c4d-4b31-8b6a-f5f2f5f55df5")
            },
            new Product
            {
                Id = Guid.Parse("b15f8d50-4c37-4f84-876d-2a9799c8b106"),
                Name = "Nimbus Phone Booth",
                Description = "Glass phone booth with integrated ventilation.",
                Price = 4999.00m,
                MinPrice = 4599.00m,
                CreatedAt = new DateTime(2024, 1, 16, 8, 45, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 16, 8, 45, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("7ca0a5de-6e7b-4efd-8e52-8b77b2c571f3")
            },
            new Product
            {
                Id = Guid.Parse("c0d3b144-e0b9-4acd-ab33-98d2cf7b8e2c"),
                Name = "Atlas Storage Cabinet",
                Description = "Secure storage cabinet with programmable keypad lock.",
                Price = 1249.00m,
                MinPrice = 1199.00m,
                CreatedAt = new DateTime(2024, 1, 17, 9, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 17, 9, 15, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("21b58d9d-4c27-46c9-8f2c-0c3a5011d8d2")
            },
            new Product
            {
                Id = Guid.Parse("d1c6db53-89a4-457e-8d4d-08e4d2bff723"),
                Name = "Harbor Bar Stool",
                Description = "Adjustable-height bar stool with swivel base.",
                Price = 279.95m,
                MinPrice = 259.95m,
                CreatedAt = new DateTime(2024, 1, 18, 10, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 18, 10, 30, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("d272ea1f-4b6b-48dd-95a5-ff4fb9c5d430")
            },
            new Product
            {
                Id = Guid.Parse("e25f330e-adc1-4144-976a-8a39633c7a7f"),
                Name = "Helios Monitor Arm",
                Description = "Dual monitor arm with gas spring and cable management.",
                Price = 459.50m,
                MinPrice = 429.50m,
                CreatedAt = new DateTime(2024, 1, 19, 11, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 19, 11, 5, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("93d9d2c3-9f4a-4c0b-bb7c-c3b8ba0fdc6d")
            },
            new Product
            {
                Id = Guid.Parse("fadf2e08-250e-4a55-8a27-8c7b902c5d99"),
                Name = "Comet Sound Mask",
                Description = "Personal sound masking fixture with warm light.",
                Price = 329.00m,
                MinPrice = 309.00m,
                CreatedAt = new DateTime(2024, 1, 20, 8, 20, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 20, 8, 20, 0, DateTimeKind.Utc),
                ConcurrencyToken = Guid.Parse("d7a96e1e-0c1b-4c0d-b6f1-0fcf8c2cf091")
            }
          );
      });

      modelBuilder.AddInboxStateEntity();
      modelBuilder.AddOutboxMessageEntity();
      modelBuilder.AddOutboxStateEntity();
    }
  }
}
