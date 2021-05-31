using Dapper;
using FitDeck.Models.UserCreated;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDeck.Repository.UserCreated
{
    public class UserCreatedRepository : IUserCreatedRepository
    {
        private readonly IConfiguration _config;
        public UserCreatedRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task AddWorkout(UserCreatedWorkout workout)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UserId", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("DayOfWeek", typeof(string));

            dataTable.Rows.Add(
                workout.UserId,
                workout.Title,
                workout.DayOfWeek);
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.OpenAsync();

                await connection.ExecuteAsync("AddWorkout",
                   new { Workout = dataTable.AsTableValuedParameter("dbo.WorkoutType") },
                   commandType: CommandType.StoredProcedure);

                workout.WorkoutId = await connection.QueryFirstAsync<int>("GetWorkoutIdByTitle",
                    new { Title = workout.Title, UserId = workout.UserId },
                    commandType: CommandType.StoredProcedure);
                foreach(int ex in workout.Exercises)
                {
                    await connection.ExecuteAsync("AddExerciseToWorkout",
                        new { WorkoutId = workout.WorkoutId, ExerciseId = ex },
                        commandType: CommandType.StoredProcedure);
                }
            }
        }

        public async Task<List<int>> GetAllExercisesFromWorkout(int workoutId)
        {
            IEnumerable<int> exercises;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.OpenAsync();

                exercises = await connection.QueryAsync<int>("GetAllExercisesFromUserCreatedWorkout",
                    new { workoutId = workoutId },
                    commandType: CommandType.StoredProcedure);
            }
            return exercises.ToList();
        }

        public async Task<List<UserCreatedRoutine>> GetAllRoutinesByUserId(int userId)
        {
            IEnumerable<UserCreatedRoutine> routines;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.OpenAsync();

                routines = await connection.QueryAsync<UserCreatedRoutine>("GetAllUserCreatedRoutinesByUserId",
                    new { UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }
            return routines.ToList();
        }

        public async Task<List<UserCreatedWorkout>> GetAllWorkoutsByUserId(int userId)
        {
            IEnumerable<UserCreatedWorkout> workouts;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.OpenAsync();

                workouts = await connection.QueryAsync<UserCreatedWorkout>("GetAllUserCreateWorkoutsByUserId",
                    new { UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }

            return workouts.ToList();
        }

        public async Task<List<UserCreatedWorkout>> GetAllWorkoutsFromRoutine(int routineId)
        {
            IEnumerable<UserCreatedWorkout> workouts;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.OpenAsync();

                workouts = await connection.QueryAsync<UserCreatedWorkout>("GetAllWorkoutsFromUserCreatedRoutine",
                    new { RoutineId = routineId },
                    commandType: CommandType.StoredProcedure);
            }
            return workouts.ToList();
        }
    }
}
