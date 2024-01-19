using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    //[Table(name: "GenresTbl", Schema = "movies")]
    public class Genre
    {
        //[Key]
        public int Id { get; set; }
        //[StringLength(maximumLength: 150)]
        //[Required]
        //[Column("GenreName")]
        public string Name { get; set; }
        public HashSet<Movie> Movies { get; set; }
    }
}

//typing 'prop' autofills a property above
