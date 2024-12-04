using System.ComponentModel.DataAnnotations;
namespace MyApp.ViewModels;



    public class EmailVM
    {
        [Required(ErrorMessage = " Email is required")]
        [EmailAddress]
        public string Email { get; set; }
       
    }

