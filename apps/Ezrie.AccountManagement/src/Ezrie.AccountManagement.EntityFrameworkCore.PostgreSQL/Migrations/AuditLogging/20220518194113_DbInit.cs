using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ezrie.AccountManagement.EntityFrameworkCore.PostgreSQL.Migrations.AuditLogging
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Event = table.Column<String>(type: "text", nullable: true),
                    Source = table.Column<String>(type: "text", nullable: true),
                    Category = table.Column<String>(type: "text", nullable: true),
                    SubjectIdentifier = table.Column<String>(type: "text", nullable: true),
                    SubjectName = table.Column<String>(type: "text", nullable: true),
                    SubjectType = table.Column<String>(type: "text", nullable: true),
                    SubjectAdditionalData = table.Column<String>(type: "text", nullable: true),
                    Action = table.Column<String>(type: "text", nullable: true),
                    Data = table.Column<String>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
