using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceCoreDapper.Migrations
{
    public partial class CreditCardAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardFName",
                table: "CreditCards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardLName",
                table: "CreditCards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardFName",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CardLName",
                table: "CreditCards");
        }
    }
}
