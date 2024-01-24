using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    public partial class ViewMovieCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW ""MovieWithCounts""

                as

                SELECT ""Id"", ""Title"",
                (SELECT count(*) FROM public.""GenreMovie"" WHERE ""MoviesId"" = public.""Movies"".""Id"" ) AS ""AmountGenres"",
                (SELECT count(distinct ""MoviesId"") FROM public.""CinemaHallMovie""
 	                INNER JOIN public.""CinemasHalls""
	                ON public.""CinemasHalls"".""Id"" = public.""CinemaHallMovie"".""CinemasHallsId""
 	                WHERE ""MoviesId"" = public.""Movies"".""Id"") AS ""AmountCinemas"",
                (SELECT count(*) FROM public.""MoviesActors"" WHERE ""MovieId"" = public.""Movies"".""Id"") AS ""AmountActors""
                FROM public.""Movies""");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW ""MovieWithCounts""");
        }
    }
}
