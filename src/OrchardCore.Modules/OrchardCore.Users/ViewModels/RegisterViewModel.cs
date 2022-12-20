using System.ComponentModel.DataAnnotations;

namespace OrchardCore.Users.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName
        {
            get
            {
                return Email;
            }
            set
            {
                Email= value;
            }
        }

        [Required(ErrorMessage = "Email is required.")]
        [Email.EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
    }
}
