using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitDeck.Models.Account
{
    public class ApplicationUserRegister : ApplicationUserLogin
    {
        [Required(ErrorMessage = "Email Required")]
        [MaxLength(50, ErrorMessage = "Email must be 5-50 characters")]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FullName Required")]
        [MinLength(5, ErrorMessage = "FullName must be 5-50 characters")]
        [MaxLength(50, ErrorMessage = "FullName must be 5-50 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Age Required")]
        [Range(0, 110, ErrorMessage ="Age must be 0-110 years old")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Height Required")]
        [Range(0, 96, ErrorMessage = "Height must be 0-96 inches tall")]
        public int HeightInches { get; set; }

        [Required(ErrorMessage = "Weight Required")]
        [Range(0, 1000, ErrorMessage = "Weight must be 0-1000 pounds")]
        public float Weight { get; set; }


    }
}
