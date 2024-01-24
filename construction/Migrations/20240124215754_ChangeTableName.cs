using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace construction.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingRequests",
                table: "BookingRequests");

            migrationBuilder.RenameTable(
                name: "BookingRequests",
                newName: "booking_requests");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "booking_requests",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "booking_requests",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_booking_requests",
                table: "booking_requests",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_booking_requests",
                table: "booking_requests");

            migrationBuilder.RenameTable(
                name: "booking_requests",
                newName: "BookingRequests");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "BookingRequests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "BookingRequests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingRequests",
                table: "BookingRequests",
                column: "id");
        }
    }
}
