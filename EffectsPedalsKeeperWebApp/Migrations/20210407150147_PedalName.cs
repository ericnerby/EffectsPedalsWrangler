using Microsoft.EntityFrameworkCore.Migrations;

namespace EffectsPedalsKeeperWebApp.Migrations
{
    public partial class PedalName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pedals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pedals");
        }
    }
}
