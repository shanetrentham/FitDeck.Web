using System;
using System.Collections.Generic;
using System.Text;

namespace FitDeck.Models.UserCreated
{
    public class UserCreatedWorkout
    {
        public int WorkoutId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string DayOfWeek { get; set; }

        public List<int> Exercises { get; set; }
    }
}
