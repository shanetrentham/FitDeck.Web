using System;
using System.Collections.Generic;
using System.Text;

namespace FitDeck.Models.UserCreated
{
    public class UserCreatedRoutine
    {
        public int WorkoutId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string RestDay { get; set; }

        public List<UserCreatedWorkout> Workouts { get; set; }

    }
}
