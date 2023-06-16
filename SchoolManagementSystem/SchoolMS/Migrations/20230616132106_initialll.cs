using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMS.Migrations
{
    /// <inheritdoc />
    public partial class initialll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Schools_SchoolId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_SchoolId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Programs");

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "Programs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "School",
                table: "Programs");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Programs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_SchoolId",
                table: "Programs",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Schools_SchoolId",
                table: "Programs",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
