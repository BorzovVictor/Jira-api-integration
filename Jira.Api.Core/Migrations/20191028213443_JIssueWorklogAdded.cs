using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira.Core.Migrations
{
    public partial class JIssueWorklogAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JiraIssueWorklogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AuthorKey = table.Column<string>(nullable: true),
                    UpdateAuthorKey = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Started = table.Column<DateTime>(nullable: true),
                    TimeSpent = table.Column<string>(nullable: true),
                    TimeSpentSeconds = table.Column<int>(nullable: false),
                    IssueId = table.Column<long>(nullable: false),
                    Self = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraIssueWorklogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraIssueWorklogs_JiraUsers_AuthorKey",
                        column: x => x.AuthorKey,
                        principalTable: "JiraUsers",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JiraIssueWorklogs_JiraUsers_UpdateAuthorKey",
                        column: x => x.UpdateAuthorKey,
                        principalTable: "JiraUsers",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JiraIssueWorklogs_AuthorKey",
                table: "JiraIssueWorklogs",
                column: "AuthorKey");

            migrationBuilder.CreateIndex(
                name: "IX_JiraIssueWorklogs_UpdateAuthorKey",
                table: "JiraIssueWorklogs",
                column: "UpdateAuthorKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JiraIssueWorklogs");
        }
    }
}
