using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitDeck.Models.Account
{
    public class ApplicationUserLogin
    {
        [Required(ErrorMessage ="UserName Required")]
        [MinLength(5, ErrorMessage ="UserName must be 5-50 characters")]
        [MaxLength(50, ErrorMessage = "UserName must be 5-50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password Required")]
        [MinLength(5, ErrorMessage ="Password must be 5-50 characters")]
        [MaxLength(50, ErrorMessage ="Password must be 5-50 characters")]
        public string Password { get; set; }
    }
}
