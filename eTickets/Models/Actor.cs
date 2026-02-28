using eTickets.Data.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor : IEntityBase 
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture URL")]
        [Required(ErrorMessage = "Profile picture URL is required")]
        //[Url(ErrorMessage = "Please enter a valid URL (e.g., http://...)")]
        public string ProfilePictureURL { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Full Name should contain only letters")]
        public string FullName { get; set; }


        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        [MinLength(10, ErrorMessage = "Biography must be at least 10 characters long")]
        public string Bio { get; set; }

        [ValidateNever]
        public List<Actor_Movie>  Actors_Movies { get; set; }
        
    }
}
