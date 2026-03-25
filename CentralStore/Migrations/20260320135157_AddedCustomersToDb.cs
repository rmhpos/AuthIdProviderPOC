using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CentralStore.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomersToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "ConcurrencyToken", "CreatedAt", "Email", "FirstName", "LastName", "Password", "StoreId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1c7d9e4a-5b6f-4a2e-8c3d-7f9a1b2c3d44"), new Guid("c5e8f1a2-3d4b-4f6a-8c9e-0b1a2d3f4e55"), new DateTime(2024, 2, 5, 9, 15, 0, 0, DateTimeKind.Utc), "ivan.georgiev@example.com", "Ivan", "Georgiev", "Password123", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 2, 6, 10, 45, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3b4c5d6e-7f8a-4b1c-9d2e-6f7a8b9c0d77"), new Guid("e9a1b2c3-d4e5-4f6a-8b9c-0d1e2f3a4b77"), new DateTime(2024, 2, 15, 8, 45, 0, 0, DateTimeKind.Utc), "georgi.ivanov@example.com", "Georgi", "Ivanov", "Password123", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 2, 16, 9, 55, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7a2e5c1d-9b3f-4c6a-8d1e-2f3a4b5c6d66"), new Guid("d7f2a3b4-5c6d-4e7f-9a0b-1c2d3e4f5a66"), new DateTime(2024, 2, 10, 14, 0, 0, 0, DateTimeKind.Utc), "elena.dimitrova@example.com", "Elena", "Dimitrova", "Password123", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 2, 11, 15, 20, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8f1a6c9e-2c4b-4a6d-9c7f-1e5a2b3d4c11"), new Guid("a3b9d2f1-6e7c-4b8a-9f01-2d3c4e5f6a77"), new DateTime(2024, 2, 1, 10, 0, 0, 0, DateTimeKind.Utc), "maria.petrova@example.com", "Maria", "Petrova", "Password123", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 2, 2, 11, 30, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
