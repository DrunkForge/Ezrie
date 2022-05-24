using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorAppWithIdentity.Server.Data.Migrations;
public partial class CreateIdentitySchema : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "AspNetRoles",
			columns: table => new
			{
				Id = table.Column<String>(type: "nvarchar(450)", nullable: false),
				Name = table.Column<String>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedName = table.Column<String>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				ConcurrencyStamp = table.Column<String>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetRoles", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUsers",
			columns: table => new
			{
				Id = table.Column<String>(type: "nvarchar(450)", nullable: false),
				UserName = table.Column<String>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedUserName = table.Column<String>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				Email = table.Column<String>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedEmail = table.Column<String>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				EmailConfirmed = table.Column<Boolean>(type: "bit", nullable: false),
				PasswordHash = table.Column<String>(type: "nvarchar(max)", nullable: true),
				SecurityStamp = table.Column<String>(type: "nvarchar(max)", nullable: true),
				ConcurrencyStamp = table.Column<String>(type: "nvarchar(max)", nullable: true),
				PhoneNumber = table.Column<String>(type: "nvarchar(max)", nullable: true),
				PhoneNumberConfirmed = table.Column<Boolean>(type: "bit", nullable: false),
				TwoFactorEnabled = table.Column<Boolean>(type: "bit", nullable: false),
				LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
				LockoutEnabled = table.Column<Boolean>(type: "bit", nullable: false),
				AccessFailedCount = table.Column<Int32>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUsers", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "DeviceCodes",
			columns: table => new
			{
				UserCode = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: false),
				DeviceCode = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: false),
				SubjectId = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: true),
				SessionId = table.Column<String>(type: "nvarchar(100)", maxLength: 100, nullable: true),
				ClientId = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: false),
				Description = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: true),
				CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
				Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
				Data = table.Column<String>(type: "nvarchar(max)", maxLength: 50480, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
			});

		migrationBuilder.CreateTable(
			name: "Keys",
			columns: table => new
			{
				Id = table.Column<String>(type: "nvarchar(450)", nullable: false),
				Version = table.Column<Int32>(type: "int", nullable: false),
				Created = table.Column<DateTime>(type: "datetime2", nullable: false),
				Use = table.Column<String>(type: "nvarchar(450)", maxLength: 450, nullable: true),
				Algorithm = table.Column<String>(type: "nvarchar(100)", maxLength: 100, nullable: false),
				IsX509Certificate = table.Column<Boolean>(type: "bit", nullable: false),
				DataProtected = table.Column<Boolean>(type: "bit", nullable: false),
				Data = table.Column<String>(type: "nvarchar(max)", maxLength: 50480, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Keys", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "PersistedGrants",
			columns: table => new
			{
				Key = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: false),
				Type = table.Column<String>(type: "nvarchar(50)", maxLength: 50, nullable: false),
				SubjectId = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: true),
				SessionId = table.Column<String>(type: "nvarchar(100)", maxLength: 100, nullable: true),
				ClientId = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: false),
				Description = table.Column<String>(type: "nvarchar(200)", maxLength: 200, nullable: true),
				CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
				Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
				ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
				Data = table.Column<String>(type: "nvarchar(max)", maxLength: 50480, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_PersistedGrants", x => x.Key);
			});

		migrationBuilder.CreateTable(
			name: "AspNetRoleClaims",
			columns: table => new
			{
				Id = table.Column<Int32>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				RoleId = table.Column<String>(type: "nvarchar(450)", nullable: false),
				ClaimType = table.Column<String>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<String>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
				table.ForeignKey(
					name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserClaims",
			columns: table => new
			{
				Id = table.Column<Int32>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				UserId = table.Column<String>(type: "nvarchar(450)", nullable: false),
				ClaimType = table.Column<String>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<String>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
				table.ForeignKey(
					name: "FK_AspNetUserClaims_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserLogins",
			columns: table => new
			{
				LoginProvider = table.Column<String>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				ProviderKey = table.Column<String>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				ProviderDisplayName = table.Column<String>(type: "nvarchar(max)", nullable: true),
				UserId = table.Column<String>(type: "nvarchar(450)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
				table.ForeignKey(
					name: "FK_AspNetUserLogins_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserRoles",
			columns: table => new
			{
				UserId = table.Column<String>(type: "nvarchar(450)", nullable: false),
				RoleId = table.Column<String>(type: "nvarchar(450)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserTokens",
			columns: table => new
			{
				UserId = table.Column<String>(type: "nvarchar(450)", nullable: false),
				LoginProvider = table.Column<String>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				Name = table.Column<String>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				Value = table.Column<String>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
				table.ForeignKey(
					name: "FK_AspNetUserTokens_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_AspNetRoleClaims_RoleId",
			table: "AspNetRoleClaims",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "RoleNameIndex",
			table: "AspNetRoles",
			column: "NormalizedName",
			unique: true,
			filter: "[NormalizedName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserClaims_UserId",
			table: "AspNetUserClaims",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserLogins_UserId",
			table: "AspNetUserLogins",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserRoles_RoleId",
			table: "AspNetUserRoles",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "EmailIndex",
			table: "AspNetUsers",
			column: "NormalizedEmail");

		migrationBuilder.CreateIndex(
			name: "UserNameIndex",
			table: "AspNetUsers",
			column: "NormalizedUserName",
			unique: true,
			filter: "[NormalizedUserName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_DeviceCodes_DeviceCode",
			table: "DeviceCodes",
			column: "DeviceCode",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_DeviceCodes_Expiration",
			table: "DeviceCodes",
			column: "Expiration");

		migrationBuilder.CreateIndex(
			name: "IX_Keys_Use",
			table: "Keys",
			column: "Use");

		migrationBuilder.CreateIndex(
			name: "IX_PersistedGrants_ConsumedTime",
			table: "PersistedGrants",
			column: "ConsumedTime");

		migrationBuilder.CreateIndex(
			name: "IX_PersistedGrants_Expiration",
			table: "PersistedGrants",
			column: "Expiration");

		migrationBuilder.CreateIndex(
			name: "IX_PersistedGrants_SubjectId_ClientId_Type",
			table: "PersistedGrants",
			columns: new[] { "SubjectId", "ClientId", "Type" });

		migrationBuilder.CreateIndex(
			name: "IX_PersistedGrants_SubjectId_SessionId_Type",
			table: "PersistedGrants",
			columns: new[] { "SubjectId", "SessionId", "Type" });
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "AspNetRoleClaims");

		migrationBuilder.DropTable(
			name: "AspNetUserClaims");

		migrationBuilder.DropTable(
			name: "AspNetUserLogins");

		migrationBuilder.DropTable(
			name: "AspNetUserRoles");

		migrationBuilder.DropTable(
			name: "AspNetUserTokens");

		migrationBuilder.DropTable(
			name: "DeviceCodes");

		migrationBuilder.DropTable(
			name: "Keys");

		migrationBuilder.DropTable(
			name: "PersistedGrants");

		migrationBuilder.DropTable(
			name: "AspNetRoles");

		migrationBuilder.DropTable(
			name: "AspNetUsers");
	}
}
