using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EQtrack.Migrations
{
    public partial class _5cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentorInventories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    toolId = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    check = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentorInventories", x => x.id);
                    table.ForeignKey(
                        name: "FK_RentorInventories_Tools_toolId",
                        column: x => x.toolId,
                        principalTable: "Tools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentorInventories_toolId",
                table: "RentorInventories",
                column: "toolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentorInventories");
        }
    }
}
