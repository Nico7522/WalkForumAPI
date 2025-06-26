using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalkForum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_onetomany_userprofile_privatemessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "PrivateMessage",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessage_UserProfileId",
                table: "PrivateMessage",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateMessage_UserProfile_UserProfileId",
                table: "PrivateMessage",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateMessage_UserProfile_UserProfileId",
                table: "PrivateMessage");

            migrationBuilder.DropIndex(
                name: "IX_PrivateMessage_UserProfileId",
                table: "PrivateMessage");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "PrivateMessage");
        }
    }
}
