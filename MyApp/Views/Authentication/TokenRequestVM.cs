using System.ComponentModel.DataAnnotations;
namespace MyApp.ViewModels;
public class TokenRequestVM
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

