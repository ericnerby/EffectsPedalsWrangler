using Microsoft.EntityFrameworkCore.Migrations;

namespace EffectsPedalsKeeperWebApp.Migrations
{
    public partial class UpdateForeignKeysAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Settings_OptionSettingId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_PedalPresets_Pedals_PedalId",
                table: "PedalPresets");

            migrationBuilder.DropForeignKey(
                name: "FK_PedalPresets_Presets_PresetId",
                table: "PedalPresets");

            migrationBuilder.DropForeignKey(
                name: "FK_Presets_PedalBoards_PedalBoardId",
                table: "Presets");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingPresets_PedalPresets_PedalPresetId",
                table: "SettingPresets");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingPresets_Settings_SettingId",
                table: "SettingPresets");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Pedals_PedalId1",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_PedalId1",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PedalId1",
                table: "Settings");

            migrationBuilder.AlterColumn<int>(
                name: "PedalId",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Settings",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SettingId",
                table: "SettingPresets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PedalPresetId",
                table: "SettingPresets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PedalBoardId",
                table: "Presets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pedals",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Maker",
                table: "Pedals",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PresetId",
                table: "PedalPresets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PedalId",
                table: "PedalPresets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OptionSettingId",
                table: "Options",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Options",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Settings_OptionSettingId",
                table: "Options",
                column: "OptionSettingId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedalPresets_Pedals_PedalId",
                table: "PedalPresets",
                column: "PedalId",
                principalTable: "Pedals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedalPresets_Presets_PresetId",
                table: "PedalPresets",
                column: "PresetId",
                principalTable: "Presets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_PedalBoards_PedalBoardId",
                table: "Presets",
                column: "PedalBoardId",
                principalTable: "PedalBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingPresets_PedalPresets_PedalPresetId",
                table: "SettingPresets",
                column: "PedalPresetId",
                principalTable: "PedalPresets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingPresets_Settings_SettingId",
                table: "SettingPresets",
                column: "SettingId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Settings_OptionSettingId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_PedalPresets_Pedals_PedalId",
                table: "PedalPresets");

            migrationBuilder.DropForeignKey(
                name: "FK_PedalPresets_Presets_PresetId",
                table: "PedalPresets");

            migrationBuilder.DropForeignKey(
                name: "FK_Presets_PedalBoards_PedalBoardId",
                table: "Presets");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingPresets_PedalPresets_PedalPresetId",
                table: "SettingPresets");

            migrationBuilder.DropForeignKey(
                name: "FK_SettingPresets_Settings_SettingId",
                table: "SettingPresets");

            migrationBuilder.AlterColumn<int>(
                name: "PedalId",
                table: "Settings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "PedalId1",
                table: "Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SettingId",
                table: "SettingPresets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PedalPresetId",
                table: "SettingPresets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PedalBoardId",
                table: "Presets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pedals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Maker",
                table: "Pedals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<int>(
                name: "PresetId",
                table: "PedalPresets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PedalId",
                table: "PedalPresets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OptionSettingId",
                table: "Options",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Options",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_PedalId1",
                table: "Settings",
                column: "PedalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Settings_OptionSettingId",
                table: "Options",
                column: "OptionSettingId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedalPresets_Pedals_PedalId",
                table: "PedalPresets",
                column: "PedalId",
                principalTable: "Pedals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedalPresets_Presets_PresetId",
                table: "PedalPresets",
                column: "PresetId",
                principalTable: "Presets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_PedalBoards_PedalBoardId",
                table: "Presets",
                column: "PedalBoardId",
                principalTable: "PedalBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingPresets_PedalPresets_PedalPresetId",
                table: "SettingPresets",
                column: "PedalPresetId",
                principalTable: "PedalPresets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SettingPresets_Settings_SettingId",
                table: "SettingPresets",
                column: "SettingId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Pedals_PedalId1",
                table: "Settings",
                column: "PedalId1",
                principalTable: "Pedals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
