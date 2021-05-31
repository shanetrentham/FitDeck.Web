using FitDeck.Models.UserCreated;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitDeck.Repository.UserCreated
{
    public interface IUserCreatedRepository
    {
        public Task<List<UserCreatedWorkout>> GetAllWorkoutsFromRoutine(int routineId);

        public Task<List<UserCreatedRoutine>> GetAllRoutinesByUserId(int userId);

        public Task<List<int>> GetAllExercisesFromWorkout(int workoutId);

        public Task<List<UserCreatedWorkout>> GetAllWorkoutsByUserId(int routineId);

        public Task AddWorkout(UserCreatedWorkout workout);

    }
}
