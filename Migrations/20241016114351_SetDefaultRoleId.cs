using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.Migrations
{
    /// <inheritdoc />
    public partial class SetDefaultRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
            name: "RoleId",
            table: "Users",
            nullable: false,
            defaultValue: 1,
            oldClrType: typeof(int));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
