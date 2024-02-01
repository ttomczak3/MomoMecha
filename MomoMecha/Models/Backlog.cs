using System.ComponentModel.DataAnnotations;

namespace MomoMecha.Models
{
    public class Backlog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Series is required")]
        public string Series { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        public string Grade { get; set; }

        [Required(ErrorMessage = "Scale is required")]
        public string Scale { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
