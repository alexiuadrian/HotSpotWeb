using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotSpotWeb.Migrations
{
    /// <inheritdoc />
    public partial class LatestUpdateToConfReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_Applications_ApplicationId",
                table: "Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependencies_Applications_ApplicationId",
                table: "Dependencies");

            migrationBuilder.DropIndex(
                name: "IX_Dependencies_ApplicationId",
                table: "Dependencies");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_ApplicationId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Dependencies");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Configurations");

            migrationBuilder.AddColumn<int>(
                name: "ConfigurationId",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ConfigurationId",
                table: "Applications",
                column: "ConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Configurations_ConfigurationId",
                table: "Applications",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Configurations_ConfigurationId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ConfigurationId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ConfigurationId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Dependencies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Configurations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dependencies_ApplicationId",
                table: "Dependencies",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ApplicationId",
                table: "Configurations",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_Applications_ApplicationId",
                table: "Configurations",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependencies_Applications_ApplicationId",
                table: "Dependencies",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");
        }
    }
}
