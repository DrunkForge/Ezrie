using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ezrie.AccountManagement.EntityFrameworkCore.SqlServer.Migrations.AuditLogging
{
	public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Int64>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    SubjectIdentifier = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    SubjectName = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    SubjectType = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    SubjectAdditionalData = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");
        }
    }
}
