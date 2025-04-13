using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant_Reservation_System_.DataAccess.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ReservationPropUpdateTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "FK_Comments_Comments_ParentId",
                table: "Comments",
                column: "ParentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Reservations_ReservationId",
                table: "Products",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reservations_ReservationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ReservationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments",
                column: "ParentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
