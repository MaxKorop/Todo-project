using System.ComponentModel.DataAnnotations;

namespace todoList.Web.Models
{
    public class UserSignUpModel
    {
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 4)]
        [Required(ErrorMessage = "Username cannot be empty.")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email cannot be empty.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        public string Password { get; set; }
    }
}