using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EQtrack.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "toolID",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "userEmail",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userEmail",
                table: "Returns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "flag",
                table: "Inventories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_toolID",
                table: "Tickets",
                column: "toolID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tools_toolID",
                table: "Tickets",
                column: "toolID",
                principalTable: "Tools",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tools_toolID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_toolID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "toolID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "userEmail",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "userEmail",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "flag",
                table: "Inventories");
        }
    }
}
