﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ezrie.AdministrationService.EntityFrameworkCore.Migrations.Migrations
{
	public partial class CreateDatabase : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AbpAuditLogs",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					ApplicationName = table.Column<String>(type: "character varying(96)", maxLength: 96, nullable: true),
					UserId = table.Column<Guid>(type: "uuid", nullable: true),
					UserName = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: true),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					TenantName = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					ImpersonatorUserId = table.Column<Guid>(type: "uuid", nullable: true),
					ImpersonatorUserName = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: true),
					ImpersonatorTenantId = table.Column<Guid>(type: "uuid", nullable: true),
					ImpersonatorTenantName = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					ExecutionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ExecutionDuration = table.Column<Int32>(type: "integer", nullable: false),
					ClientIpAddress = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					ClientName = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: true),
					ClientId = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					CorrelationId = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					BrowserInfo = table.Column<String>(type: "character varying(512)", maxLength: 512, nullable: true),
					HttpMethod = table.Column<String>(type: "character varying(16)", maxLength: 16, nullable: true),
					Url = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: true),
					Exceptions = table.Column<String>(type: "text", nullable: true),
					Comments = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: true),
					HttpStatusCode = table.Column<Int32>(type: "integer", nullable: true),
					ExtraProperties = table.Column<String>(type: "text", nullable: true),
					ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpAuditLogs", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpClaimTypes",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					Required = table.Column<Boolean>(type: "boolean", nullable: false),
					IsStatic = table.Column<Boolean>(type: "boolean", nullable: false),
					Regex = table.Column<String>(type: "character varying(512)", maxLength: 512, nullable: true),
					RegexDescription = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: true),
					Description = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: true),
					ValueType = table.Column<Int32>(type: "integer", nullable: false),
					ExtraProperties = table.Column<String>(type: "text", nullable: true),
					ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpClaimTypes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpFeatureValues",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					Value = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					ProviderName = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					ProviderKey = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpFeatureValues", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpLinkUsers",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					SourceUserId = table.Column<Guid>(type: "uuid", nullable: false),
					SourceTenantId = table.Column<Guid>(type: "uuid", nullable: true),
					TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
					TargetTenantId = table.Column<Guid>(type: "uuid", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpLinkUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpOrganizationUnits",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					ParentId = table.Column<Guid>(type: "uuid", nullable: true),
					Code = table.Column<String>(type: "character varying(95)", maxLength: 95, nullable: false),
					DisplayName = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					ExtraProperties = table.Column<String>(type: "text", nullable: true),
					ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: true),
					CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
					LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
					IsDeleted = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
					DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
					DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpOrganizationUnits", x => x.Id);
					table.ForeignKey(
						name: "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId",
						column: x => x.ParentId,
						principalTable: "AbpOrganizationUnits",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "AbpPermissionGrants",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					Name = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					ProviderName = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: false),
					ProviderKey = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpPermissionGrants", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpRoles",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					Name = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					NormalizedName = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					IsDefault = table.Column<Boolean>(type: "boolean", nullable: false),
					IsStatic = table.Column<Boolean>(type: "boolean", nullable: false),
					IsPublic = table.Column<Boolean>(type: "boolean", nullable: false),
					ExtraProperties = table.Column<String>(type: "text", nullable: true),
					ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpSecurityLogs",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					ApplicationName = table.Column<String>(type: "character varying(96)", maxLength: 96, nullable: true),
					Identity = table.Column<String>(type: "character varying(96)", maxLength: 96, nullable: true),
					Action = table.Column<String>(type: "character varying(96)", maxLength: 96, nullable: true),
					UserId = table.Column<Guid>(type: "uuid", nullable: true),
					UserName = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: true),
					TenantName = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					ClientId = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					CorrelationId = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					ClientIpAddress = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					BrowserInfo = table.Column<String>(type: "character varying(512)", maxLength: 512, nullable: true),
					CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ExtraProperties = table.Column<String>(type: "text", nullable: true),
					ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpSecurityLogs", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpSettings",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					Value = table.Column<String>(type: "character varying(2048)", maxLength: 2048, nullable: false),
					ProviderName = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					ProviderKey = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpSettings", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpUsers",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					UserName = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					NormalizedUserName = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					Name = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					Surname = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: true),
					Email = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					NormalizedEmail = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					EmailConfirmed = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
					PasswordHash = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: true),
					SecurityStamp = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					IsExternal = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
					PhoneNumber = table.Column<String>(type: "character varying(16)", maxLength: 16, nullable: true),
					PhoneNumberConfirmed = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
					IsActive = table.Column<Boolean>(type: "boolean", nullable: false),
					TwoFactorEnabled = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
					LockoutEnabled = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
					AccessFailedCount = table.Column<Int32>(type: "integer", nullable: false, defaultValue: 0),
					ExtraProperties = table.Column<String>(type: "text", nullable: true),
					ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: true),
					CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
					LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
					LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
					IsDeleted = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
					DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
					DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AbpAuditLogActions",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
					ServiceName = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: true),
					MethodName = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: true),
					Parameters = table.Column<String>(type: "character varying(2000)", maxLength: 2000, nullable: true),
					ExecutionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ExecutionDuration = table.Column<Int32>(type: "integer", nullable: false),
					ExtraProperties = table.Column<String>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpAuditLogActions", x => x.Id);
					table.ForeignKey(
						name: "FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId",
						column: x => x.AuditLogId,
						principalTable: "AbpAuditLogs",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpEntityChanges",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					ChangeTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					ChangeType = table.Column<Byte>(type: "smallint", nullable: false),
					EntityTenantId = table.Column<Guid>(type: "uuid", nullable: true),
					EntityId = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					EntityTypeFullName = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					ExtraProperties = table.Column<String>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpEntityChanges", x => x.Id);
					table.ForeignKey(
						name: "FK_AbpEntityChanges_AbpAuditLogs_AuditLogId",
						column: x => x.AuditLogId,
						principalTable: "AbpAuditLogs",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpOrganizationUnitRoles",
				columns: table => new
				{
					RoleId = table.Column<Guid>(type: "uuid", nullable: false),
					OrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpOrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
					table.ForeignKey(
						name: "FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationU~",
						column: x => x.OrganizationUnitId,
						principalTable: "AbpOrganizationUnits",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AbpOrganizationUnitRoles_AbpRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AbpRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpRoleClaims",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					RoleId = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					ClaimType = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					ClaimValue = table.Column<String>(type: "character varying(1024)", maxLength: 1024, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AbpRoleClaims_AbpRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AbpRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpUserClaims",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					ClaimType = table.Column<String>(type: "character varying(256)", maxLength: 256, nullable: false),
					ClaimValue = table.Column<String>(type: "character varying(1024)", maxLength: 1024, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AbpUserClaims_AbpUsers_UserId",
						column: x => x.UserId,
						principalTable: "AbpUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpUserLogins",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					LoginProvider = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					ProviderKey = table.Column<String>(type: "character varying(196)", maxLength: 196, nullable: false),
					ProviderDisplayName = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpUserLogins", x => new { x.UserId, x.LoginProvider });
					table.ForeignKey(
						name: "FK_AbpUserLogins_AbpUsers_UserId",
						column: x => x.UserId,
						principalTable: "AbpUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpUserOrganizationUnits",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					OrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpUserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
					table.ForeignKey(
						name: "FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationU~",
						column: x => x.OrganizationUnitId,
						principalTable: "AbpOrganizationUnits",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AbpUserOrganizationUnits_AbpUsers_UserId",
						column: x => x.UserId,
						principalTable: "AbpUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpUserRoles",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					RoleId = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AbpUserRoles_AbpRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AbpRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AbpUserRoles_AbpUsers_UserId",
						column: x => x.UserId,
						principalTable: "AbpUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpUserTokens",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					LoginProvider = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: false),
					Name = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					Value = table.Column<String>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AbpUserTokens_AbpUsers_UserId",
						column: x => x.UserId,
						principalTable: "AbpUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AbpEntityPropertyChanges",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					TenantId = table.Column<Guid>(type: "uuid", nullable: true),
					EntityChangeId = table.Column<Guid>(type: "uuid", nullable: false),
					NewValue = table.Column<String>(type: "character varying(512)", maxLength: 512, nullable: true),
					OriginalValue = table.Column<String>(type: "character varying(512)", maxLength: 512, nullable: true),
					PropertyName = table.Column<String>(type: "character varying(128)", maxLength: 128, nullable: false),
					PropertyTypeFullName = table.Column<String>(type: "character varying(64)", maxLength: 64, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbpEntityPropertyChanges", x => x.Id);
					table.ForeignKey(
						name: "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId",
						column: x => x.EntityChangeId,
						principalTable: "AbpEntityChanges",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_AbpAuditLogActions_AuditLogId",
				table: "AbpAuditLogActions",
				column: "AuditLogId");

			migrationBuilder.CreateIndex(
				name: "IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_Executio~",
				table: "AbpAuditLogActions",
				columns: new[] { "TenantId", "ServiceName", "MethodName", "ExecutionTime" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpAuditLogs_TenantId_ExecutionTime",
				table: "AbpAuditLogs",
				columns: new[] { "TenantId", "ExecutionTime" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpAuditLogs_TenantId_UserId_ExecutionTime",
				table: "AbpAuditLogs",
				columns: new[] { "TenantId", "UserId", "ExecutionTime" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpEntityChanges_AuditLogId",
				table: "AbpEntityChanges",
				column: "AuditLogId");

			migrationBuilder.CreateIndex(
				name: "IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId",
				table: "AbpEntityChanges",
				columns: new[] { "TenantId", "EntityTypeFullName", "EntityId" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpEntityPropertyChanges_EntityChangeId",
				table: "AbpEntityPropertyChanges",
				column: "EntityChangeId");

			migrationBuilder.CreateIndex(
				name: "IX_AbpFeatureValues_Name_ProviderName_ProviderKey",
				table: "AbpFeatureValues",
				columns: new[] { "Name", "ProviderName", "ProviderKey" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~",
				table: "AbpLinkUsers",
				columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId",
				table: "AbpOrganizationUnitRoles",
				columns: new[] { "RoleId", "OrganizationUnitId" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpOrganizationUnits_Code",
				table: "AbpOrganizationUnits",
				column: "Code");

			migrationBuilder.CreateIndex(
				name: "IX_AbpOrganizationUnits_ParentId",
				table: "AbpOrganizationUnits",
				column: "ParentId");

			migrationBuilder.CreateIndex(
				name: "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey",
				table: "AbpPermissionGrants",
				columns: new[] { "TenantId", "Name", "ProviderName", "ProviderKey" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_AbpRoleClaims_RoleId",
				table: "AbpRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "IX_AbpRoles_NormalizedName",
				table: "AbpRoles",
				column: "NormalizedName");

			migrationBuilder.CreateIndex(
				name: "IX_AbpSecurityLogs_TenantId_Action",
				table: "AbpSecurityLogs",
				columns: new[] { "TenantId", "Action" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpSecurityLogs_TenantId_ApplicationName",
				table: "AbpSecurityLogs",
				columns: new[] { "TenantId", "ApplicationName" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpSecurityLogs_TenantId_Identity",
				table: "AbpSecurityLogs",
				columns: new[] { "TenantId", "Identity" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpSecurityLogs_TenantId_UserId",
				table: "AbpSecurityLogs",
				columns: new[] { "TenantId", "UserId" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpSettings_Name_ProviderName_ProviderKey",
				table: "AbpSettings",
				columns: new[] { "Name", "ProviderName", "ProviderKey" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_AbpUserClaims_UserId",
				table: "AbpUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AbpUserLogins_LoginProvider_ProviderKey",
				table: "AbpUserLogins",
				columns: new[] { "LoginProvider", "ProviderKey" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId",
				table: "AbpUserOrganizationUnits",
				columns: new[] { "UserId", "OrganizationUnitId" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpUserRoles_RoleId_UserId",
				table: "AbpUserRoles",
				columns: new[] { "RoleId", "UserId" });

			migrationBuilder.CreateIndex(
				name: "IX_AbpUsers_Email",
				table: "AbpUsers",
				column: "Email");

			migrationBuilder.CreateIndex(
				name: "IX_AbpUsers_NormalizedEmail",
				table: "AbpUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "IX_AbpUsers_NormalizedUserName",
				table: "AbpUsers",
				column: "NormalizedUserName");

			migrationBuilder.CreateIndex(
				name: "IX_AbpUsers_UserName",
				table: "AbpUsers",
				column: "UserName");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AbpAuditLogActions");

			migrationBuilder.DropTable(
				name: "AbpClaimTypes");

			migrationBuilder.DropTable(
				name: "AbpEntityPropertyChanges");

			migrationBuilder.DropTable(
				name: "AbpFeatureValues");

			migrationBuilder.DropTable(
				name: "AbpLinkUsers");

			migrationBuilder.DropTable(
				name: "AbpOrganizationUnitRoles");

			migrationBuilder.DropTable(
				name: "AbpPermissionGrants");

			migrationBuilder.DropTable(
				name: "AbpRoleClaims");

			migrationBuilder.DropTable(
				name: "AbpSecurityLogs");

			migrationBuilder.DropTable(
				name: "AbpSettings");

			migrationBuilder.DropTable(
				name: "AbpUserClaims");

			migrationBuilder.DropTable(
				name: "AbpUserLogins");

			migrationBuilder.DropTable(
				name: "AbpUserOrganizationUnits");

			migrationBuilder.DropTable(
				name: "AbpUserRoles");

			migrationBuilder.DropTable(
				name: "AbpUserTokens");

			migrationBuilder.DropTable(
				name: "AbpEntityChanges");

			migrationBuilder.DropTable(
				name: "AbpOrganizationUnits");

			migrationBuilder.DropTable(
				name: "AbpRoles");

			migrationBuilder.DropTable(
				name: "AbpUsers");

			migrationBuilder.DropTable(
				name: "AbpAuditLogs");
		}
	}
}
