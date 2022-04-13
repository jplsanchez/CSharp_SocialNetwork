using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infra.Migrations
{
    public partial class Adminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("32a3f8cb-da79-400a-8736-59ac00d0f929"), "c95892ff-1270-41e2-ac34-863254203c65", "Default", "DEFAULT" },
                    { new Guid("52a688e7-a80f-42f3-8834-d7d1afb83948"), "eeb89262-dfb6-4e5a-a79e-f55670415a7e", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("eac00691-dc64-4c25-ab7d-e9869d79a358"), 0, "8211f5ec-c6ea-4d77-9cf3-da0211d71498", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEBaYWcpSJT4mLHf22BUaQeEoLkeBj/qm2zX5R6mJ4niAGuVljcXr4aA31UdZo1rUig==", null, false, "1426a81b-bc16-4fbd-9644-a6f507c71213", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("32a3f8cb-da79-400a-8736-59ac00d0f929"), new Guid("eac00691-dc64-4c25-ab7d-e9869d79a358") },
                    { new Guid("52a688e7-a80f-42f3-8834-d7d1afb83948"), new Guid("eac00691-dc64-4c25-ab7d-e9869d79a358") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("32a3f8cb-da79-400a-8736-59ac00d0f929"), new Guid("eac00691-dc64-4c25-ab7d-e9869d79a358") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("52a688e7-a80f-42f3-8834-d7d1afb83948"), new Guid("eac00691-dc64-4c25-ab7d-e9869d79a358") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("32a3f8cb-da79-400a-8736-59ac00d0f929"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52a688e7-a80f-42f3-8834-d7d1afb83948"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eac00691-dc64-4c25-ab7d-e9869d79a358"));
        }
    }
}
