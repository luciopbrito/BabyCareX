using System.ComponentModel.DataAnnotations;
using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "The email field is required"), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "The password field is required")]
        public string Password { get; set; }
    }
}