using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagementPortal.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeviceIPAddressAddedInEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceIPAddress",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                column: "DeviceIPAddress",
                value: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2,
                column: "DeviceIPAddress",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceIPAddress",
                table: "Employees");
        }
    }
}
