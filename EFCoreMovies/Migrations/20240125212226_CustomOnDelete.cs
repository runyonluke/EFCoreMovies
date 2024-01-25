using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    public partial class CustomOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasHalls_Cinemas_CinemaId",
                table: "CinemasHalls");

            migrationBuilder.AlterColumn<int>(
                name: "CinemaId",
                table: "CinemasHalls",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasHalls_Cinemas_CinemaId",
                table: "CinemasHalls",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasHalls_Cinemas_CinemaId",
                table: "CinemasHalls");

            migrationBuilder.AlterColumn<int>(
                name: "CinemaId",
                table: "CinemasHalls",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasHalls_Cinemas_CinemaId",
                table: "CinemasHalls",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id");
        }
    }
}
