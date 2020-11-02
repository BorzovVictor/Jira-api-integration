using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira.Core.Migrations
{
    public partial class AddedJiraProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JiraProjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    LeadKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    ProjectTypeKey = table.Column<string>(nullable: true),
                    Simplified = table.Column<bool>(nullable: false),
                    Style = table.Column<string>(nullable: true),
                    IsPrivate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraProjects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JiraProjects");
        }
    }
}
