namespace EFCoreMovies.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public CinemaHallType CinemaHallType { get; set; }
        public decimal Cost { get; set; }
        public Currency Currency { get; set; }
        public int CinemaId { get; set; }
        //if we wanted to use a key that isn't configured by convention
        //[ForeignKey(nameof(NameOfKey))]
        public Cinema Cinema { get; set; }
        public HashSet<Movie> Movies { get; set; }
    }
}
