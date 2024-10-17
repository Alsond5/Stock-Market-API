using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.Migrations
{
    /// <inheritdoc />
    public partial class EditSomeModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalStockQuantity",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStocks",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalStockQuantity",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "TotalStocks",
                table: "Portfolios");
        }
    }
}
