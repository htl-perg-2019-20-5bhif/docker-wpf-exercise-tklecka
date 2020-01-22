using Microsoft.EntityFrameworkCore.Migrations;

namespace docker_wpf_exercise_tklecka.Migrations
{
    public partial class delete_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Reservations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Reservations",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
