using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ezrie.AccountManagement.EntityFrameworkCore.SqlServer.Migrations.Logging
{
	public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Int64>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<String>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Exception = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    LogEvent = table.Column<String>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<String>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
