using Microsoft.EntityFrameworkCore.Migrations;

namespace PASR.Migrations
{
    public partial class PASR_initial_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cgc",
                table: "Leads",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_Cgc",
                table: "Leads",
                column: "Cgc",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Leads_Cgc",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Cgc",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Leads");
        }
    }
}
