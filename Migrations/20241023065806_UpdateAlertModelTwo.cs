using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAlertModelTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTriggered",
                table: "Alerts",
                newName: "WasTriggeredBelow");

            migrationBuilder.AddColumn<bool>(
                name: "WasTriggeredAbove",
                table: "Alerts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasTriggeredAbove",
                table: "Alerts");

            migrationBuilder.RenameColumn(
                name: "WasTriggeredBelow",
                table: "Alerts",
                newName: "IsTriggered");
        }
    }
}
