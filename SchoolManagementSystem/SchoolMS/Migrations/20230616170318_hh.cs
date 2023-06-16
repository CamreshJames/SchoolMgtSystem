using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMS.Migrations
{
    /// <inheritdoc />
    public partial class hh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Lecturer");

            migrationBuilder.RenameColumn(
                name: "NationalID",
                table: "PendingLecturers",
                newName: "NationalId");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "PendingLecturers",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NationalId",
                table: "PendingLecturers",
                newName: "NationalID");

            migrationBuilder.AlterColumn<long>(
                name: "NationalID",
                table: "PendingLecturers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "Lecturer",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
