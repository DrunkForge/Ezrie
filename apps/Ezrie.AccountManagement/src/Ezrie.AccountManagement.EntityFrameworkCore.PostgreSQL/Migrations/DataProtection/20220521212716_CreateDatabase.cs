using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ezrie.AccountManagement.EntityFrameworkCore.PostgreSQL.Migrations.DataProtection
{
	public partial class CreateDatabase : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "DataProtectionKeys",
				columns: table => new
				{
					Id = table.Column<Int32>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					FriendlyName = table.Column<String>(type: "text", nullable: true),
					Xml = table.Column<String>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DataProtectionKeys");
		}
	}
}
