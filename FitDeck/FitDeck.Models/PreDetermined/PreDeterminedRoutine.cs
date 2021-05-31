using System;
using System.Collections.Generic;
using System.Text;

namespace FitDeck.Models.PreDetermined
{
    public class PreDeterminedRoutine
    {
        public int RoutineId { get; set; }

        public string Title { get; set; }

        public string RestDay { get; set; }

        public List<PreDeterminedWorkout> Workouts { get; set; }
    }
}
