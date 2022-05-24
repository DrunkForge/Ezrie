using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ezrie.AccountManagement.EntityFrameworkCore.MySql.Migrations.AuditLogging
{
	public partial class CreateDatabase : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterDatabase()
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateTable(
				name: "AuditLog",
				columns: table => new
				{
					Id = table.Column<Int64>(type: "bigint", nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					Event = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Source = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Category = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					SubjectIdentifier = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					SubjectName = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					SubjectType = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					SubjectAdditionalData = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Action = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Data = table.Column<String>(type: "longtext", nullable: true)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Created = table.Column<DateTime>(type: "datetime(6)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AuditLog", x => x.Id);
				})
				.Annotation("MySql:CharSet", "utf8mb4");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AuditLog");
		}
	}
}
