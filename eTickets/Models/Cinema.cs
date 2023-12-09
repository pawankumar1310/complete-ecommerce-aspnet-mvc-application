using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage ="Cinema logo is required")]
        public string Logo { get; set; }
        [Display(Name = "Name" )]
        [Required(ErrorMessage ="Cinema name is required")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage ="Description is required")]
        public string Description { get; set; }

        //Relationship
        public List<Movie> Movies { get; set; }
    }
}
