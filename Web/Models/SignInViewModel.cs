using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SignInViewModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "First name must be atleast 2 characters long!")]
        [RegularExpression(@"[A-Za-z\-\']{2,30}", ErrorMessage = "Input is not a name!")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Last name must be atleast 2 characters long!")]
        [RegularExpression(@"[A-Za-z\-\']{2,30}", ErrorMessage = "Input is not a name!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [RegularExpression(@"^(?:("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+\/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?:(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Input is not an email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 32 characters long!")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,32}$", ErrorMessage = "Password must contain atleast one numeric value!")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required!")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Confirm password must be between 8 and 32 characters long!")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,32}$", ErrorMessage = "Password must contain atleast one numeric value!")]
        public string ConfirmPassword { get; set; }
    }
}
