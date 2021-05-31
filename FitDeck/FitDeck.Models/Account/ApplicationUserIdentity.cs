using System;
using System.Collections.Generic;
using System.Text;

namespace FitDeck.Models.Account
{
    public class ApplicationUserIdentity
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int HeightInches { get; set; }

        public float Weight { get; set; }

        public string PasswordHash { get; set; }
    }
}
