using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace secondProject.Migrations
{
    /// <inheritdoc />
    public partial class setHotelModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Hotels",
                newName: "ReserveNow");

            migrationBuilder.AddColumn<bool>(
                name: "FreeCancellation",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeCancellation",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "ReserveNow",
                table: "Hotels",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
