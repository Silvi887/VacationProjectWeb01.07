using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationAdd.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapacityAdultsCount",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CapacityChildrenCount",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Facilities",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomSize",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "View",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookedRoomsids",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressHotel",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoomCount",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    HotelRoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => new { x.RoomID, x.HotelID });
                    table.ForeignKey(
                        name: "FK_HotelRooms_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "IdHotel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "IdRoom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b723a1a-a790-43bf-a779-b80a4fe2e859", "AQAAAAIAAYagAAAAEE0ytnbVehEk5gaSplSeBJlCFwLI0FaddesvVicVWJgAdGaoSxov+fBZFesvrcpjxA==", "1121de99-386a-410a-a223-ae369ac8f3cb" });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_HotelID",
                table: "HotelRooms",
                column: "HotelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "CapacityAdultsCount",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CapacityChildrenCount",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Facilities",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomSize",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "View",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BookedRoomsids",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AddressHotel",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "RoomCount",
                table: "Hotels");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17739900-056b-4224-8145-81bbf903daa9", "AQAAAAIAAYagAAAAEPyjVtMZ/qQTvSPOuVKdlNDTY1eXY+ODCbeVjaQzUvvozfQZsxfrvtKZw0gdJYM9GQ==", "aa0b7fb1-a688-42fd-9301-1637c340b27c" });
        }
    }
}
