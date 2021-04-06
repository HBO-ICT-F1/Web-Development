using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_Development.Migrations
{
    public partial class AlterProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("CatalogNumber", "records");
            migrationBuilder.AddColumn<string>(
                name: "CatalogNumber",
                table: "records",
                type: "varchar(255)");
            
            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "products",
                type: "varchar(255)");
            
            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "products",
                type: "varchar(255)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("CatalogNumber", "records");
            migrationBuilder.AddColumn<int>(
                name: "CatalogNumber",
                table: "records",
                type: "int(11)");
            
            migrationBuilder.DropColumn("Format", "products");
            migrationBuilder.DropColumn("Label", "products");
        }
    }
}
