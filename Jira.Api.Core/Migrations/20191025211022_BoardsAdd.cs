using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira.Core.Migrations
{
    public partial class BoardsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JiraBoards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Self = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraBoards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraBoards_JiraProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "JiraProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JiraBoards_ProjectId",
                table: "JiraBoards",
                column: "ProjectId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JiraBoards");
        }
    }
}
