using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CentralStore.LocalStore.Migrations
{
    /// <inheritdoc />
    public partial class SeededProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ConcurrencyToken", "CreatedAt", "Description", "MinPrice", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1a8c30a4-0445-4b1f-b01c-4a5c8f4d832f"), new Guid("bfe2f7fd-3551-4d13-9fcd-d0c1bebafd00"), new DateTime(2024, 1, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Large solid maple conference table with integrated cable troughs.", 1299.99m, "Aurora Conference Table", 1399.99m, new DateTime(2024, 1, 4, 9, 15, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2f5d93e7-1b60-4687-9baa-c2aa1c5dc65a"), new Guid("75cbf198-7ffb-4ee2-9b26-17fa16cbd59e"), new DateTime(2024, 1, 5, 13, 30, 0, 0, DateTimeKind.Utc), "Ergonomic lounge chair upholstered in perforated leather.", 599.00m, "Lumen Lounge Chair", 649.50m, new DateTime(2024, 1, 5, 16, 45, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3662d4ac-1a6f-447d-80b0-4673c7c7fc56"), new Guid("8f6e54f6-4ab9-40b3-8592-7c5d0d4d1a2e"), new DateTime(2024, 1, 6, 9, 20, 0, 0, DateTimeKind.Utc), "Height-adjustable standing desk with waterfall edge and programmable presets.", 899.95m, "Northwind Standing Desk", 999.95m, new DateTime(2024, 1, 6, 9, 25, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4442e177-f6ea-4b77-9f07-4f44d7f1b137"), new Guid("3d54c37c-61b9-4139-a6ab-149226c95ebb"), new DateTime(2024, 1, 7, 7, 10, 0, 0, DateTimeKind.Utc), "LED task lamp offering tunable white light and touch dimming.", 109.99m, "Solstice Task Lamp", 129.99m, new DateTime(2024, 1, 7, 7, 10, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5bacb48d-43d6-4f06-9cf8-16c1f8be6b66"), new Guid("97b3f65a-099e-4a6e-bfd8-1c3e679a5b87"), new DateTime(2024, 1, 8, 14, 0, 0, 0, DateTimeKind.Utc), "Modular reception sofa with antimicrobial fabric.", 1799.00m, "Vela Reception Sofa", 1899.00m, new DateTime(2024, 1, 9, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("686b45f0-cb33-4d03-b040-6a8d966b0f29"), new Guid("a6d4eed6-9e7b-4bfa-92c6-2e63a2dc4d5e"), new DateTime(2024, 1, 10, 11, 15, 0, 0, DateTimeKind.Utc), "Wall-mounted acoustic panel with felt finish.", 229.00m, "Cascade Acoustic Panel", 249.00m, new DateTime(2024, 1, 10, 11, 15, 0, 0, DateTimeKind.Utc) },
                    { new Guid("77cc8d8e-bfa1-4f4d-b594-0bb9f8ce7a8a"), new Guid("0c2df9b7-dd68-4e96-8718-ffbb9b5b1a75"), new DateTime(2024, 1, 12, 15, 0, 0, 0, DateTimeKind.Utc), "Steel mobile pedestal with silent casters and lock.", 349.25m, "Harbor Mobile Pedestal", 379.25m, new DateTime(2024, 1, 12, 15, 15, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8237c4d1-2805-4f65-9c37-18452f2fe14b"), new Guid("bd6c731f-5083-4bed-8f36-54d4efbf8f90"), new DateTime(2024, 1, 13, 10, 30, 0, 0, DateTimeKind.Utc), "High-back executive chair with synchronous tilt and leather.", 749.99m, "Mercury Executive Chair", 799.99m, new DateTime(2024, 1, 13, 10, 35, 0, 0, DateTimeKind.Utc) },
                    { new Guid("92ea71b4-6f7b-4c76-a0bf-aee9871d2a78"), new Guid("65e5a1b6-0b25-4de1-b61f-11cad0ff6a47"), new DateTime(2024, 1, 14, 9, 0, 0, 0, DateTimeKind.Utc), "Modular shelving system with powder-coated frames.", 429.75m, "Zenith Modular Shelf", 459.75m, new DateTime(2024, 1, 14, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a240d279-c935-4392-becf-2eceaab4e52d"), new Guid("f6ef3101-2c4d-4b31-8b6a-f5f2f5f55df5"), new DateTime(2024, 1, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Magnetic dry-erase board with aluminum frame.", 199.99m, "Vertex Whiteboard", 219.99m, new DateTime(2024, 1, 15, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b15f8d50-4c37-4f84-876d-2a9799c8b106"), new Guid("7ca0a5de-6e7b-4efd-8e52-8b77b2c571f3"), new DateTime(2024, 1, 16, 8, 45, 0, 0, DateTimeKind.Utc), "Glass phone booth with integrated ventilation.", 4599.00m, "Nimbus Phone Booth", 4999.00m, new DateTime(2024, 1, 16, 8, 45, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c0d3b144-e0b9-4acd-ab33-98d2cf7b8e2c"), new Guid("21b58d9d-4c27-46c9-8f2c-0c3a5011d8d2"), new DateTime(2024, 1, 17, 9, 0, 0, 0, DateTimeKind.Utc), "Secure storage cabinet with programmable keypad lock.", 1199.00m, "Atlas Storage Cabinet", 1249.00m, new DateTime(2024, 1, 17, 9, 15, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d1c6db53-89a4-457e-8d4d-08e4d2bff723"), new Guid("d272ea1f-4b6b-48dd-95a5-ff4fb9c5d430"), new DateTime(2024, 1, 18, 10, 30, 0, 0, DateTimeKind.Utc), "Adjustable-height bar stool with swivel base.", 259.95m, "Harbor Bar Stool", 279.95m, new DateTime(2024, 1, 18, 10, 30, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e25f330e-adc1-4144-976a-8a39633c7a7f"), new Guid("93d9d2c3-9f4a-4c0b-bb7c-c3b8ba0fdc6d"), new DateTime(2024, 1, 19, 11, 0, 0, 0, DateTimeKind.Utc), "Dual monitor arm with gas spring and cable management.", 429.50m, "Helios Monitor Arm", 459.50m, new DateTime(2024, 1, 19, 11, 5, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fadf2e08-250e-4a55-8a27-8c7b902c5d99"), new Guid("d7a96e1e-0c1b-4c0d-b6f1-0fcf8c2cf091"), new DateTime(2024, 1, 20, 8, 20, 0, 0, DateTimeKind.Utc), "Personal sound masking fixture with warm light.", 309.00m, "Comet Sound Mask", 329.00m, new DateTime(2024, 1, 20, 8, 20, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1a8c30a4-0445-4b1f-b01c-4a5c8f4d832f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2f5d93e7-1b60-4687-9baa-c2aa1c5dc65a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3662d4ac-1a6f-447d-80b0-4673c7c7fc56"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4442e177-f6ea-4b77-9f07-4f44d7f1b137"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5bacb48d-43d6-4f06-9cf8-16c1f8be6b66"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("686b45f0-cb33-4d03-b040-6a8d966b0f29"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("77cc8d8e-bfa1-4f4d-b594-0bb9f8ce7a8a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8237c4d1-2805-4f65-9c37-18452f2fe14b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("92ea71b4-6f7b-4c76-a0bf-aee9871d2a78"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a240d279-c935-4392-becf-2eceaab4e52d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b15f8d50-4c37-4f84-876d-2a9799c8b106"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c0d3b144-e0b9-4acd-ab33-98d2cf7b8e2c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d1c6db53-89a4-457e-8d4d-08e4d2bff723"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e25f330e-adc1-4144-976a-8a39633c7a7f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fadf2e08-250e-4a55-8a27-8c7b902c5d99"));
        }
    }
}
