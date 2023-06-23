using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMS.Migrations
{
    /// <inheritdoc />
    public partial class clubsM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubsMembers_Clubs_ClubId",
                table: "ClubsMembers");

            migrationBuilder.DropIndex(
                name: "IX_ClubsMembers_ClubId",
                table: "ClubsMembers");

            migrationBuilder.RenameColumn(
                name: "ClubId",
                table: "ClubsMembers",
                newName: "ClubName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClubName",
                table: "ClubsMembers",
                newName: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsMembers_ClubId",
                table: "ClubsMembers",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubsMembers_Clubs_ClubId",
                table: "ClubsMembers",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "ClubId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
