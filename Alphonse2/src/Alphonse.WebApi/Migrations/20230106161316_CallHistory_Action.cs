using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alphonse.WebApi.Migrations
{
    public partial class CallHistory_Action : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "CallHistories",
                type: "TEXT",
                unicode: false,
                maxLength: 12,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "CallHistories");
        }
    }
}
