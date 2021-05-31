using FitDeck.Models.PreDetermined;
using FitDeck.Repository.PreDetermined;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitDeck.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreDeterminedController : ControllerBase
    {
        private readonly IPreDeterminedRepository _preDeterminedRepository;
        public PreDeterminedController(IPreDeterminedRepository preDeterminedRepository)
        {
            _preDeterminedRepository = preDeterminedRepository;
        }

        [HttpGet("getRoutines")]
        public async Task<ActionResult<List<PreDeterminedRoutine>>> GetRoutines()
        {
            var routines = await _preDeterminedRepository.GetAllRoutinesAsync();

            return Ok(routines);
        }

        [HttpPost("getWorkoutsFromRoutine")]
        public async Task<ActionResult<List<PreDeterminedWorkout>>> GetWorkoutsFromRoutine(int routineId)
        {
            var workouts = await _preDeterminedRepository.GetWorkoutsFromRoutineAsync(routineId);

            return Ok(workouts);
        }
        //Maybe update with ExRx api call
        //returns exercise ids for now
        [HttpPost("getExercisesFromWorkout")]
        public async Task<ActionResult<List<int>>> GetExercisesFromWorkout(int workoutId)
        {
            var exercises = await _preDeterminedRepository.GetExerciseFromWorkout(workoutId);

            return Ok(exercises);
        }

        [HttpGet("getWorkouts")]
        public async Task<ActionResult<List<int>>> GetWorkouts()
        {
            var workouts = await _preDeterminedRepository.GetAllWorkouts();

            return Ok(workouts);
        }
    }
}
