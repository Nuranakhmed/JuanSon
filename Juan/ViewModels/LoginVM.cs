using System.ComponentModel.DataAnnotations;

namespace Juan.ViewModels
{
    public class LoginVM   //Login Actionun View-suna bu modeli vereceyik
    {
        [Required]
        public string? UserNameOrEmail { get; set; }
        [Required,DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
