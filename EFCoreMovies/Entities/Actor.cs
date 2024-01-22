using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace EFCoreMovies.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                // tOm hOLLanD => Tom Holland
                _name = string.Join(' ',
                    value.Split(' ')
                        .Select(n => n[0].ToString().ToUpper() + n.Substring(1).ToLower())
                        .ToArray());
            }
        }
        public string Biography { get; set; }
        //[Column(TypeName = "Date")]
        //public Nullable<DateTime> DateOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public HashSet<MovieActor> MoviesActors { get; set; }
    }
}
