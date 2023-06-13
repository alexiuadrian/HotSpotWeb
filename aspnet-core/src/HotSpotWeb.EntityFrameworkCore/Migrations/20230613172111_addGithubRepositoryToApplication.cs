using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotSpotWeb.Migrations
{
    /// <inheritdoc />
    public partial class addGithubRepositoryToApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GithubRepositoryId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_GithubRepositoryId",
                table: "Applications",
                column: "GithubRepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_GithubRepositories_GithubRepositoryId",
                table: "Applications",
                column: "GithubRepositoryId",
                principalTable: "GithubRepositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_GithubRepositories_GithubRepositoryId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_GithubRepositoryId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "GithubRepositoryId",
                table: "Applications");
        }
    }
}
