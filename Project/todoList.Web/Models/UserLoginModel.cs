using System.ComponentModel.DataAnnotations;

namespace todoList.Web.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username cannot be empty.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        public string Password { get; set; }
    }
}