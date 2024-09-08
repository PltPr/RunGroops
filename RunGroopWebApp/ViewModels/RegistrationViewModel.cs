using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Repeat passowrd is required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password do not match")]
        public string RepeatPassword { get; set; }
    }
}
