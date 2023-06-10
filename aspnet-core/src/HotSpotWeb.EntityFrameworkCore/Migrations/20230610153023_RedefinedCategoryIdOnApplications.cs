using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotSpotWeb.Migrations
{
    /// <inheritdoc />
    public partial class RedefinedCategoryIdOnApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Configurations_ConfigurationId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Configurations_ConfigurationId",
                table: "Applications",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Configurations_ConfigurationId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationId",
                table: "Applications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Configurations_ConfigurationId",
                table: "Applications",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id");
        }
    }
}
