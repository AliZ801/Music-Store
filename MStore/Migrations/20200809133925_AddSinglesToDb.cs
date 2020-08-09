using Microsoft.EntityFrameworkCore.Migrations;

namespace MStore.Migrations
{
    public partial class AddSinglesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Singles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Artist = table.Column<string>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    ReleasedDate = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Singles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Singles_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Singles_GenreId",
                table: "Singles",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Singles");
        }
    }
}
