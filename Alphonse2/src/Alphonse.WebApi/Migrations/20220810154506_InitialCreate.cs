using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alphonse.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallHistories",
                columns: table => new
                {
                    CallHistoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<long>(type: "INTEGER", nullable: false),
                    UCallNumber = table.Column<string>(type: "TEXT", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallHistories", x => x.CallHistoryId);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    PhoneNumberId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UPhoneNumber = table.Column<string>(type: "TEXT", unicode: false, maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Allowed = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.PhoneNumberId);
                });

            migrationBuilder.CreateIndex(
                name: "Unicity_UPhoneNumber",
                table: "PhoneNumbers",
                column: "UPhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallHistories");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");
        }
    }
}
