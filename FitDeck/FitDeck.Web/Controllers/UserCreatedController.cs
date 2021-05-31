using FitDeck.Repository.UserCreated;
using FitDeck.Models.UserCreated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace FitDeck.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCreatedController : ControllerBase
    {
        private readonly IUserCreatedRepository _userCreatedRepository;

        public UserCreatedController(IUserCreatedRepository userCreatedRepository)
        {
            _userCreatedRepository = userCreatedRepository;
        }

        [HttpPost("getWorkoutsFromRoutine")]
        public async Task<ActionResult<List<UserCreatedWorkout>>> GetWorkoutsFromRoutine(int routineId)
        {
            var workouts = await _userCreatedRepository.GetAllWorkoutsFromRoutine(routineId);

            return Ok(workouts);
        }
        [Authorize]
        [HttpGet("getRoutinesByUserId")]
        public async Task<ActionResult<List<UserCreatedRoutine>>> GetAllRoutinesByUserId()
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var routines = await _userCreatedRepository.GetAllRoutinesByUserId(applicationUserId);

            return Ok(routines);
        }
        //gets exercise id
        //TODO: change to call ExRx api
        [HttpPost("getExercisesFromWorkout")]
        public async Task<ActionResult<List<int>>> GetAllExercisesInWorkout(int workoutId)
        {
            var exercises = await _userCreatedRepository.GetAllExercisesFromWorkout(workoutId);

            return Ok(exercises);
        }

        [Authorize]
        [HttpGet("getAllWorkoutsByUserId")]
        public async Task<ActionResult<List<UserCreatedWorkout>>> GetAllWorkoutsByUserId()
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var workouts = await _userCreatedRepository.GetAllWorkoutsByUserId(applicationUserId);

            return Ok(workouts);
        }

        [Authorize]
        [HttpPost("addWorkout")]
        public async Task<ActionResult> AddWorkout(UserCreatedWorkout workout)
        {
            workout.UserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            await _userCreatedRepository.AddWorkout(workout);

            return Ok();
        }
    }
}
