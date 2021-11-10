using Microsoft.EntityFrameworkCore.Migrations;

namespace PASR.Migrations
{
    public partial class CallInterestedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Intersted",
                table: "Calls",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intersted",
                table: "Calls");
        }
    }
}
