using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira.Core.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JiraUsers",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Self = table.Column<string>(nullable: true),
                    AccountId = table.Column<string>(nullable: true),
                    AccountType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TimeZone = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(nullable: true),
                    AvatarUrl = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraUsers", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JiraUsers");
        }
    }
}
