using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira.Core.Migrations
{
    public partial class BoardsKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JiraBoards_ProjectId",
                table: "JiraBoards");

            migrationBuilder.CreateIndex(
                name: "IX_JiraBoards_ProjectId",
                table: "JiraBoards",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JiraBoards_ProjectId",
                table: "JiraBoards");

            migrationBuilder.CreateIndex(
                name: "IX_JiraBoards_ProjectId",
                table: "JiraBoards",
                column: "ProjectId",
                unique: true);
        }
    }
}
