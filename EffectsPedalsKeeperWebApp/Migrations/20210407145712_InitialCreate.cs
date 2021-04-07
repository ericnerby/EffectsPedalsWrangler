using Microsoft.EntityFrameworkCore.Migrations;

namespace EffectsPedalsKeeperWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedalBoards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedalBoards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PedalBoardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presets_PedalBoards_PedalBoardId",
                        column: x => x.PedalBoardId,
                        principalTable: "PedalBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedalBoardPedals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedalBoardId = table.Column<int>(type: "int", nullable: false),
                    PedalId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedalBoardPedals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedalBoardPedals_PedalBoards_PedalBoardId",
                        column: x => x.PedalBoardId,
                        principalTable: "PedalBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedalBoardPedals_Pedals_PedalId",
                        column: x => x.PedalId,
                        principalTable: "Pedals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettingType = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false),
                    PedalId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PedalId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Pedals_PedalId",
                        column: x => x.PedalId,
                        principalTable: "Pedals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Settings_Pedals_PedalId1",
                        column: x => x.PedalId1,
                        principalTable: "Pedals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedalPresets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Engaged = table.Column<bool>(type: "bit", nullable: false),
                    PresetId = table.Column<int>(type: "int", nullable: true),
                    PedalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedalPresets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedalPresets_Pedals_PedalId",
                        column: x => x.PedalId,
                        principalTable: "Pedals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PedalPresets_Presets_PresetId",
                        column: x => x.PresetId,
                        principalTable: "Presets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    OptionSettingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Settings_OptionSettingId",
                        column: x => x.OptionSettingId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SettingPresets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    SettingId = table.Column<int>(type: "int", nullable: true),
                    PedalPresetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingPresets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingPresets_PedalPresets_PedalPresetId",
                        column: x => x.PedalPresetId,
                        principalTable: "PedalPresets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SettingPresets_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionSettingId",
                table: "Options",
                column: "OptionSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_PedalBoardPedals_PedalBoardId",
                table: "PedalBoardPedals",
                column: "PedalBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_PedalBoardPedals_PedalId",
                table: "PedalBoardPedals",
                column: "PedalId");

            migrationBuilder.CreateIndex(
                name: "IX_PedalPresets_PedalId",
                table: "PedalPresets",
                column: "PedalId");

            migrationBuilder.CreateIndex(
                name: "IX_PedalPresets_PresetId",
                table: "PedalPresets",
                column: "PresetId");

            migrationBuilder.CreateIndex(
                name: "IX_Presets_PedalBoardId",
                table: "Presets",
                column: "PedalBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingPresets_PedalPresetId",
                table: "SettingPresets",
                column: "PedalPresetId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingPresets_SettingId",
                table: "SettingPresets",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_PedalId",
                table: "Settings",
                column: "PedalId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_PedalId1",
                table: "Settings",
                column: "PedalId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "PedalBoardPedals");

            migrationBuilder.DropTable(
                name: "SettingPresets");

            migrationBuilder.DropTable(
                name: "PedalPresets");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Presets");

            migrationBuilder.DropTable(
                name: "Pedals");

            migrationBuilder.DropTable(
                name: "PedalBoards");
        }
    }
}
