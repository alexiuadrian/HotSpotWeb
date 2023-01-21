using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotSpotWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_Applications_ApplicationId",
                table: "Dependency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dependency",
                table: "Dependency");

            migrationBuilder.RenameTable(
                name: "Dependency",
                newName: "Dependencies");

            migrationBuilder.RenameIndex(
                name: "IX_Dependency_ApplicationId",
                table: "Dependencies",
                newName: "IX_Dependencies_ApplicationId");

            migrationBuilder.AddColumn<int>(
                name: "ConfigurationId",
                table: "Dependencies",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dependencies",
                table: "Dependencies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Framework = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dependencies_ConfigurationId",
                table: "Dependencies",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ApplicationId",
                table: "Configurations",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependencies_Applications_ApplicationId",
                table: "Dependencies",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependencies_Configurations_ConfigurationId",
                table: "Dependencies",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependencies_Applications_ApplicationId",
                table: "Dependencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependencies_Configurations_ConfigurationId",
                table: "Dependencies");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dependencies",
                table: "Dependencies");

            migrationBuilder.DropIndex(
                name: "IX_Dependencies_ConfigurationId",
                table: "Dependencies");

            migrationBuilder.DropColumn(
                name: "ConfigurationId",
                table: "Dependencies");

            migrationBuilder.RenameTable(
                name: "Dependencies",
                newName: "Dependency");

            migrationBuilder.RenameIndex(
                name: "IX_Dependencies_ApplicationId",
                table: "Dependency",
                newName: "IX_Dependency_ApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dependency",
                table: "Dependency",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_Applications_ApplicationId",
                table: "Dependency",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");
        }
    }
}
