using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotSpotWeb.Migrations
{
    /// <inheritdoc />
    public partial class addApplicationToGithubRepository : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "GithubRepositories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GithubRepositories_ApplicationId",
                table: "GithubRepositories",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_GithubRepositories_Applications_ApplicationId",
                table: "GithubRepositories",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GithubRepositories_Applications_ApplicationId",
                table: "GithubRepositories");

            migrationBuilder.DropIndex(
                name: "IX_GithubRepositories_ApplicationId",
                table: "GithubRepositories");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "GithubRepositories");
        }
    }
}
