using FitDeck.Models.Account;
using FitDeck.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitDeck.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUserIdentity> _userManager;
        private readonly SignInManager<ApplicationUserIdentity> _signInManager;

        public AccountController(
            ITokenService tokenService,
            UserManager<ApplicationUserIdentity> userManager,
            SignInManager<ApplicationUserIdentity> signInManager
            )
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> Register(ApplicationUserRegister applicationUserRegister)
        {
            var applicationUserIdentity = new ApplicationUserIdentity()
            {
                UserName = applicationUserRegister.UserName,
                Email = applicationUserRegister.Email,
                FullName = applicationUserRegister.FullName,
                Age = applicationUserRegister.Age,
                HeightInches = applicationUserRegister.HeightInches,
                Weight = applicationUserRegister.Weight
            };

            var result = await _userManager.CreateAsync(applicationUserIdentity, applicationUserRegister.Password);

            if(result.Succeeded)
            {
                applicationUserIdentity = await _userManager.FindByNameAsync(applicationUserRegister.UserName);

                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserId = applicationUserIdentity.UserID,
                    Username = applicationUserIdentity.UserName,
                    Email = applicationUserIdentity.Email,
                    FullName = applicationUserIdentity.FullName,
                    Age = applicationUserIdentity.Age,
                    HeightInches = applicationUserIdentity.HeightInches,
                    Weight = applicationUserIdentity.Weight,
                    Token = _tokenService.CreateToken(applicationUserIdentity)
                };

                return Ok(applicationUser);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> Login(ApplicationUserLogin applicationUserLogin)
        {
            var applicationUserIdentity = await _userManager.FindByNameAsync(applicationUserLogin.UserName);

            if(applicationUserIdentity != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(applicationUserIdentity, applicationUserLogin.Password, false);

                if(result.Succeeded)
                {
                    ApplicationUser applicationUser = new ApplicationUser
                    {
                        UserId = applicationUserIdentity.UserID,
                        Username = applicationUserIdentity.UserName,
                        Email = applicationUserIdentity.Email,
                        FullName = applicationUserIdentity.FullName,
                        Age = applicationUserIdentity.Age,
                        HeightInches = applicationUserIdentity.HeightInches,
                        Weight = applicationUserIdentity.Weight,
                        Token = _tokenService.CreateToken(applicationUserIdentity)
                    };

                    return Ok(applicationUser);
                }
            }

            return BadRequest("Invalid login attempt.");
        }
    }
}
