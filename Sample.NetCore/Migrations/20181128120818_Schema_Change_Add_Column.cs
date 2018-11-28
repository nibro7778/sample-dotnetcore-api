using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.API.Migrations
{
    public partial class Schema_Change_Add_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                table: "ContactPerson",
                maxLength: 12,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNo",
                table: "ContactPerson");
        }
    }
}
