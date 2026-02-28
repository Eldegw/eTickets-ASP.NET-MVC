using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class MovieViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "Movie name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.1, 1000, ErrorMessage = "Price must be between 0.1 and 1000")]
        public double Price { get; set; }

        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required")]
        //[Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageURL { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select Category")]
        [Required(ErrorMessage = "Please select a category")]
        public MovieCategory MovieCategory { get; set; }

        // Relationships
        [Display(Name = "Select Actors")]
        [Required(ErrorMessage = "Please select at least one actor")]
        public List<int> ActorsId { get; set; }

        [Display(Name = "Select Cinema")]
        [Required(ErrorMessage = "Cinema is required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select Producer")]
        [Required(ErrorMessage = "Producer is required")]
        public int ProducerId { get; set; }
    }
}