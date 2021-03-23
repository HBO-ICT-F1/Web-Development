using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Web_Development.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "records",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Artist = table.Column<string>("varchar(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>("varchar(255)", maxLength: 255, nullable: false),
                    YoutubeToken = table.Column<string>("varchar(255)", maxLength: 255, nullable: true),
                    Year = table.Column<short>("smallint", nullable: false),
                    CatalogNumber = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_records", x => x.Id); });

            migrationBuilder.CreateTable(
                "users",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>("varchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>("varchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<short>("smallint", nullable: false),
                    Password = table.Column<string>("varchar(60)", maxLength: 60, nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime", nullable: false),
                    Address = table.Column<string>("text", nullable: false),
                    PostalCode = table.Column<string>("text", nullable: false),
                    Country = table.Column<string>("text", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_users", x => x.Id); });

            migrationBuilder.CreateTable(
                "products",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RecordId = table.Column<long>("bigint", nullable: false),
                    UserId = table.Column<long>("bigint", nullable: false),
                    Price = table.Column<double>("double", nullable: false),
                    ForSale = table.Column<bool>("tinyint(1)", nullable: false),
                    PlateCondition = table.Column<int>("int", nullable: false),
                    SleeveCondition = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        "FK_products_records_RecordId",
                        x => x.RecordId,
                        "records",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_products_users_UserId",
                        x => x.UserId,
                        "users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "sales",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>("bigint", nullable: false),
                    UserId = table.Column<long>("bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.Id);
                    table.ForeignKey(
                        "FK_sales_products_ProductId",
                        x => x.ProductId,
                        "products",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_sales_users_UserId",
                        x => x.UserId,
                        "users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_products_RecordId",
                "products",
                "RecordId");

            migrationBuilder.CreateIndex(
                "IX_products_UserId",
                "products",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_sales_ProductId",
                "sales",
                "ProductId");

            migrationBuilder.CreateIndex(
                "IX_sales_UserId",
                "sales",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "sales");

            migrationBuilder.DropTable(
                "products");

            migrationBuilder.DropTable(
                "records");

            migrationBuilder.DropTable(
                "users");
        }
    }
}