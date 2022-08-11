using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EQtrack.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Inventories_InventoryId2",
                table: "Returns");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Inventories_InventoryId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_InventoryId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Returns_InventoryId2",
                table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_RentorInventories_InventoryId",
                table: "RentorInventories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_InventoryId1",
                table: "Tickets",
                column: "InventoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_InventoryId2",
                table: "Returns",
                column: "InventoryId2");

            migrationBuilder.CreateIndex(
                name: "IX_RentorInventories_InventoryId",
                table: "RentorInventories",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Inventories_InventoryId2",
                table: "Returns",
                column: "InventoryId2",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Inventories_InventoryId1",
                table: "Tickets",
                column: "InventoryId1",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
