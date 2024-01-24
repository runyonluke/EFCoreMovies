using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    //[Table(name: "GenresTbl", Schema = "movies")]
    //[Index(nameof(Name), IsUnique = true)]
    public class Genre
    {
        //[Key]
        public int Id { get; set; }
        //[StringLength(maximumLength: 150)]
        //[Required]
        //[Column("GenreName")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public HashSet<Movie> Movies { get; set; }
    }
}

//typing 'prop' autofills a property above
