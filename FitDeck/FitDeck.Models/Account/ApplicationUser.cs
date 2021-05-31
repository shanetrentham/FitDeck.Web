using System;
using System.Collections.Generic;
using System.Text;

namespace FitDeck.Models.Account
{
    public class ApplicationUser
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FullName  { get; set; }

        public int Age { get; set; }

        public int HeightInches { get; set; }
        public float Weight  { get; set; }

        public string Token { get; set; }
    }
}
