using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GO.Service.Users.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Roles = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LockedEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ArchivedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserConnection",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CurrentScope = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnection", x => new { x.UserId, x.Type });
                    table.ForeignKey(
                        name: "FK_UserConnection_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ArchivedAt", "ArchivedBy", "CreatedAt", "FirstName", "LastName", "LockedBy", "LockedEnd", "Roles" },
                values: new object[] { new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"), null, null, new DateTimeOffset(new DateTime(2022, 11, 29, 8, 0, 56, 126, DateTimeKind.Unspecified).AddTicks(8558), new TimeSpan(0, 0, 0, 0, 0)), "Dmytro", "😇", null, null, (byte)2 });

            migrationBuilder.InsertData(
                table: "UserConnection",
                columns: new[] { "Type", "UserId", "CurrentScope", "ExternalId", "Nickname" },
                values: new object[] { 1, new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"), 2, "428296956", "Nimb84" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConnection");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
