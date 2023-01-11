using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotSpotWeb.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTenantIdToUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Applications");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Applications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
