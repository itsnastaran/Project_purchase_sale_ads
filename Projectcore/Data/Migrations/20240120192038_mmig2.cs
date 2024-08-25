using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectcore.Data.Migrations
{
    public partial class mmig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
               name: "NameFamily",
               table: "AspNetUsers",
               type: "nvarchar(256)",
               maxLength: 256,
               nullable: true,
               defaultValue: "",
               oldClrType: typeof(string),
               oldType: "nvarchar(256)",
               oldMaxLength: 256,
               oldNullable: false);

           
        

    }

    protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameFamily",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
