using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatchesWebApp.Data.Migrations
{
    public partial class DropSeasonColumnFromLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Season",
                table: "Leagues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Season",
                table: "Leagues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
