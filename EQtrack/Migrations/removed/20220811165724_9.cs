using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EQtrack.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Inventories_InventoryId2",
                table: "Returns");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId2",
                table: "Returns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "RentorInventories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Inventories_InventoryId2",
                table: "Returns",
                column: "InventoryId2",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Inventories_InventoryId2",
                table: "Returns");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId2",
                table: "Returns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "RentorInventories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
        }
    }
}
