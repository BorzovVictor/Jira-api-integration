using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira.Core.Migrations
{
    public partial class JStatusesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JiraStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IconUrl = table.Column<string>(nullable: true),
                    Self = table.Column<string>(nullable: true),
                    JStatusCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraStatuses_JiraStatusCategory_JStatusCategoryId",
                        column: x => x.JStatusCategoryId,
                        principalTable: "JiraStatusCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JiraStatuses_JStatusCategoryId",
                table: "JiraStatuses",
                column: "JStatusCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JiraStatuses");
        }
    }
}
