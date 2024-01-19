using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        //[Unicode(false)]
        public string PosterURL { get; set; }
        public HashSet<Genre> Genres { get; set; }
        public HashSet<CinemaHall> CinemasHalls { get; set; }
        public HashSet<MovieActor> MoviesActors { get; set; }
    }
}
