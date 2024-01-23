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
        //doesn't track in database
        [NotMapped]
        public int? Age
        {
            get
            {
                if (!DateOfBirth.HasValue)
                {
                    return null;
                }

                var dob = DateOfBirth.Value;

                var age = DateTime.Today.Year - dob.Year;

                if (new DateTime(DateTime.Today.Year, dob.Month, dob.Day) > DateTime.Today)
                    age--;

                return age;
            }
        }
        public HashSet<MovieActor> MoviesActors { get; set; }
        public Address Address { get; set; }
    }
}
