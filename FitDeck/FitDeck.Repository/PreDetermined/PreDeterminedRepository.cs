using Dapper;
using FitDeck.Models.PreDetermined;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDeck.Repository.PreDetermined
{
    public class PreDeterminedRepository : IPreDeterminedRepository
    {
        private readonly IConfiguration _config;
        public PreDeterminedRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<PreDeterminedRoutine>> GetAllRoutinesAsync()
        {
            IEnumerable<PreDeterminedRoutine> routines;

            using (var connection = new SqlConnection())
            {
                await connection.OpenAsync();

                routines = await connection.QueryAsync<PreDeterminedRoutine>("GetAllPreDeterminedRoutines",
                    new { },
                    commandType: CommandType.StoredProcedure);
                foreach (PreDeterminedRoutine pdr in routines)
                {
                    pdr.Workouts = await GetWorkoutsFromRoutineAsync(pdr.RoutineId);
                }
            }
            return routines.ToList();
        }

        public async Task<List<PreDeterminedWorkout>> GetWorkoutsFromRoutineAsync(int routineId)
        {
            IEnumerable<PreDeterminedWorkout> workouts;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                workouts = await connection.QueryAsync<PreDeterminedWorkout>("GetAllWorkoutsFromPreDeterminedRoutines",
                    new { RoutineId = routineId },
                    commandType: CommandType.StoredProcedure);
                //Add all corresponding exerciseIds from the PreDeterminedWorkouts_Exercises table
                foreach(PreDeterminedWorkout pdw in workouts)
                {

                    pdw.Exercises = await GetExerciseFromWorkout(pdw.WorkoutId);
                }
            }

            return workouts.ToList();
        }

        public async Task<List<PreDeterminedWorkout>> GetAllWorkouts()
        {
            IEnumerable<PreDeterminedWorkout> workouts;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                workouts = await connection.QueryAsync<PreDeterminedWorkout>("GetAllPreDeterminedWorkouts",
                    new { },
                    commandType: CommandType.StoredProcedure);

                foreach (PreDeterminedWorkout pdw in workouts)
                {
                    pdw.Exercises = await GetExerciseFromWorkout(pdw.WorkoutId);
                }
            }

            return workouts.ToList();
        }

        public async Task<List<int>> GetExerciseFromWorkout(int workoutId)
        {
            IEnumerable<int> exercises;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                exercises = await connection.QueryAsync<int>("GetAllExercisesFromPreDeterminedWorkout",
                    new { WorkoutId = workoutId },
                    commandType: CommandType.StoredProcedure);
            }
            return exercises.ToList();
        }
    }
}
