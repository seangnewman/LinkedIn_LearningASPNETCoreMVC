using System.ComponentModel.DataAnnotations;

namespace ExploreCalifornia.Controllers
{
    public class CreatePostRequest
    {
        [Display(Name = "Post Title")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters long")]
        public string Title { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "Blog posts must be at least 100 characters long")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}
