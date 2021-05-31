using FitDeck.Models.Workouts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitDeck.Models.Account
{
    public class ApplicationUser
    {
        public int ApplicationUserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public float Height { get; set; }

        public int Weight { get; set; }

        //Date of birth
        public DateTime DOB { get; set; }
        //List of User created workouts made by the user
        public List<UserCreatedWorkout> UserCreatedWorkouts { get; set; }
        //List of predetermined workouts saved by the user
        public List<PreDeterminedWorkout> PreDeterminedWorkouts { get; set; }

        public string Token { get; set; }
    }
}
