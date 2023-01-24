using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Juan.ViewModels
{
    public class RegisterVM
    {
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string  Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [Required,Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
