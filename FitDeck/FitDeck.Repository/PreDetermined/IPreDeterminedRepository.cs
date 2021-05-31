using FitDeck.Models.PreDetermined;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitDeck.Repository.PreDetermined
{
    public interface IPreDeterminedRepository
    {
        public Task<List<PreDeterminedWorkout>> GetWorkoutsFromRoutineAsync(int routineId);

        public Task<List<PreDeterminedRoutine>> GetAllRoutinesAsync();

        public Task<List<int>> GetExerciseFromWorkout(int workoutId);

        public Task<List<PreDeterminedWorkout>> GetAllWorkouts();
    }
}
