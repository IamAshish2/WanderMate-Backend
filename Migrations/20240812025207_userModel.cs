using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace secondProject.Migrations
{
    /// <inheritdoc />
    public partial class userModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConformPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConformPassword",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");
        }
    }
}
