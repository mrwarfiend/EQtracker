using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EQtrack.Migrations
{
    public partial class _12ishTimeNewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DamagedItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    toolId = table.Column<int>(type: "int", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    repairNeeded = table.Column<bool>(type: "bit", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: true),
                    AdminId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timeStamp2 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamagedItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_DamagedItems_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_DamagedItems_Tools_toolId",
                        column: x => x.toolId,
                        principalTable: "Tools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamagedItems_InventoryId",
                table: "DamagedItems",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DamagedItems_toolId",
                table: "DamagedItems",
                column: "toolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamagedItems");
        }
    }
}
