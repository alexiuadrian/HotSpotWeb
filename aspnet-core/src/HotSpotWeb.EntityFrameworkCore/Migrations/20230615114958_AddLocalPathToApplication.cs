using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotSpotWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalPathToApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalPath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalPath",
                table: "Applications");
        }
    }
}
