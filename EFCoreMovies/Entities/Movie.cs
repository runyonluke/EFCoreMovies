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
        public List<Genre> Genres { get; set; }
        public List<CinemaHall> CinemasHalls { get; set; }
        public List<MovieActor> MoviesActors { get; set; }

        internal object Select(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
