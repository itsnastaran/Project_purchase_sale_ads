using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectcore.Data.Migrations
{
    public partial class mmig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
               name: "Postalcode",
               table: "AspNetUsers",
               type: "nvarchar(10)",
               maxLength: 10,
               nullable: true,
               defaultValue: "",
               oldClrType: typeof(string),
               oldType: "nvarchar(10)",
               oldMaxLength: 10,
               oldNullable: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Postalcode",
                table: "AspNetUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
