using eTickets.Models;

namespace eTickets.Data.MovieModels
    {
    public class NewMovieDropdownVM
        {
        public NewMovieDropdownVM()
            {
            producers = new List<Producer>();
            actors = new List<Actor>();
            cinemas = new List<Cinema>();
            }
        public List<Producer> producers {get; set;}
        public List<Actor> actors { get; set; }
        public List<Cinema> cinemas { get; set; }
        }
    }
