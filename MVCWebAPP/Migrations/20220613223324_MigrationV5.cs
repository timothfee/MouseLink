using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCWebAPP.Migrations
{
    public partial class MigrationV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MouseMouseUser",
                columns: table => new
                {
                    favoriteMiceId = table.Column<int>(type: "int", nullable: false),
                    userVoteId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouseMouseUser", x => new { x.favoriteMiceId, x.userVoteId });
                    table.ForeignKey(
                        name: "FK_MouseMouseUser_AspNetUsers_userVoteId",
                        column: x => x.userVoteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MouseMouseUser_Mice_favoriteMiceId",
                        column: x => x.favoriteMiceId,
                        principalTable: "Mice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MouseMouseUser_userVoteId",
                table: "MouseMouseUser",
                column: "userVoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MouseMouseUser");
        }
    }
}
