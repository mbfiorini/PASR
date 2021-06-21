using Microsoft.EntityFrameworkCore.Migrations;

namespace PASR.Migrations
{
    public partial class PASR_Initial_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cgc",
                table: "Leads",
                newName: "IdentityCode");

            migrationBuilder.RenameIndex(
                name: "IX_Leads_Cgc",
                table: "Leads",
                newName: "IX_Leads_IdentityCode");

            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Teams",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "TeamDescription",
                table: "Teams",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Leads",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Leads");

            migrationBuilder.RenameColumn(
                name: "IdentityCode",
                table: "Leads",
                newName: "Cgc");

            migrationBuilder.RenameIndex(
                name: "IX_Leads_IdentityCode",
                table: "Leads",
                newName: "IX_Leads_Cgc");

            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Teams",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "TeamDescription",
                table: "Teams",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }
    }
}
