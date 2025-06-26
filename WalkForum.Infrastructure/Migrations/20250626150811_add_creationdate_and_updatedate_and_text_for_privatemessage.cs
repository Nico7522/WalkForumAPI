using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalkForum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_creationdate_and_updatedate_and_text_for_privatemessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "PrivateMessage",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "PrivateMessage",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "text",
                table: "PrivateMessage",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "PrivateMessage");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "PrivateMessage");

            migrationBuilder.DropColumn(
                name: "text",
                table: "PrivateMessage");
        }
    }
}
