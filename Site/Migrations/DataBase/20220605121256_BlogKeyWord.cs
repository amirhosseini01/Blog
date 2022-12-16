using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site.Migrations.DataBase
{
    public partial class BlogKeyWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyWords",
                table: "Blogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyWords",
                table: "Blogs");
        }
    }
}
