using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WalkForum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_private_discussion_private_messages_and_relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrivateDiscussion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateDiscussion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateDiscussionUserProfile",
                columns: table => new
                {
                    PrivateDiscussionsId = table.Column<int>(type: "integer", nullable: false),
                    UserProfilesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateDiscussionUserProfile", x => new { x.PrivateDiscussionsId, x.UserProfilesId });
                    table.ForeignKey(
                        name: "FK_PrivateDiscussionUserProfile_PrivateDiscussion_PrivateDiscu~",
                        column: x => x.PrivateDiscussionsId,
                        principalTable: "PrivateDiscussion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivateDiscussionUserProfile_UserProfile_UserProfilesId",
                        column: x => x.UserProfilesId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrivateDiscussionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateMessage_PrivateDiscussion_PrivateDiscussionId",
                        column: x => x.PrivateDiscussionId,
                        principalTable: "PrivateDiscussion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateDiscussionUserProfile_UserProfilesId",
                table: "PrivateDiscussionUserProfile",
                column: "UserProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessage_PrivateDiscussionId",
                table: "PrivateMessage",
                column: "PrivateDiscussionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivateDiscussionUserProfile");

            migrationBuilder.DropTable(
                name: "PrivateMessage");

            migrationBuilder.DropTable(
                name: "PrivateDiscussion");
        }
    }
}
