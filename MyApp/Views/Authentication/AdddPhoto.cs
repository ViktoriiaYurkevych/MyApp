using System.ComponentModel.DataAnnotations;

namespace MyApp.Views.Authentication
{
    public class AdddPhoto
    {
        [Required]
        public IFormFile CoverPhoto { get; set; }
    }
}
