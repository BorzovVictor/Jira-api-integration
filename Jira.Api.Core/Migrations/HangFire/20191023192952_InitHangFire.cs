using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jira.Core.Migrations.HangFire
{
    public partial class InitHangFire : Migration
    {
        private string readMigrationSqlFile(string filename)
        {
            var t = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(t, "Sqls", filename);
            return File.ReadAllText(path);
        }
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string installHangFireSql = readMigrationSqlFile("install.sql");
            migrationBuilder.Sql(installHangFireSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string dropHangFireSql = readMigrationSqlFile("dropHangFire.sql");
            migrationBuilder.Sql(dropHangFireSql);
        }
    }
}
