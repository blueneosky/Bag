using Alphonse.WebApi.Services;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alphonse.WebApi.Migrations
{
    public partial class User_AccessRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rights",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "AccessRole",
                table: "Users",
                type: "TEXT",
                unicode: false,
                maxLength: 32,
                nullable: false,
                defaultValue: AccessRoleService.ROLE_USER);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessRole",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "Rights",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
