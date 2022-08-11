using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EQtrack.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories");*/
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Inventories_InventoryId",
                table: "Returns");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Inventories_InventoryId",
                table: "Tickets");
            */

            migrationBuilder.DropIndex(
                name: "IX_Returns_InventoryId",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Returns");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "Tickets",
                newName: "InventoryId1");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_InventoryId",
                table: "Tickets",
                newName: "IX_Tickets_InventoryId1");

            migrationBuilder.AddColumn<int>(
                name: "InventoryId2",
                table: "Returns",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "RentorInventories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_InventoryId2",
                table: "Returns",
                column: "InventoryId2");
            /*
            migrationBuilder.AddForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Inventories_InventoryId2",
                table: "Returns",
                column: "InventoryId2",
                principalTable: "Inventories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Inventories_InventoryId1",
                table: "Tickets",
                column: "InventoryId1",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Inventories_InventoryId2",
                table: "Returns");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Inventories_InventoryId1",
                table: "Tickets");*/

            migrationBuilder.DropIndex(
                name: "IX_Returns_InventoryId2",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "InventoryId2",
                table: "Returns");

            migrationBuilder.RenameColumn(
                name: "InventoryId1",
                table: "Tickets",
                newName: "InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_InventoryId1",
                table: "Tickets",
                newName: "IX_Tickets_InventoryId");

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Returns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "RentorInventories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Returns_InventoryId",
                table: "Returns",
                column: "InventoryId");

            /*
            migrationBuilder.AddForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Inventories_InventoryId",
                table: "Returns",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Inventories_InventoryId",
                table: "Tickets",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            */
        }
    }
}
