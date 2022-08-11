using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EQtrack.Migrations
{
    public partial class _6return : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Returns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "RentorInventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "RentorInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_InventoryId",
                table: "Tickets",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_InventoryId",
                table: "Returns",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RentorInventories_InventoryId",
                table: "RentorInventories",
                column: "InventoryId");
/*
            migrationBuilder.AddForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Inventories_InventoryId",
                table: "Returns",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Inventories_InventoryId",
                table: "Tickets",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction); */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentorInventories_Inventories_InventoryId",
                table: "RentorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Inventories_InventoryId",
                table: "Returns");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Inventories_InventoryId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_InventoryId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Returns_InventoryId",
                table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_RentorInventories_InventoryId",
                table: "RentorInventories");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "RentorInventories");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "RentorInventories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
