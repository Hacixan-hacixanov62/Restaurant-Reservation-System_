using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant_Reservation_System_.DataAccess.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MessageMigrationDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reservations_ReservationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ReservationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ReservationId",
                table: "Products",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Reservations_ReservationId",
                table: "Products",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
