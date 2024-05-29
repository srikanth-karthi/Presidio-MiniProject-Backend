using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Models;
using Job_Portal_Application.Exceptions;
using System;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.UserDto;
using Microsoft.AspNetCore.Authorization;
using Job_Portal_Application.Interfaces.IService;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/User")]
    [ExcludeFromCodeCoverage]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> Register(UserRegisterDto userDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var customErrorResponse = new
                {
                    Title = "One or more validation errors occurred.",
                    Errors = errors
                };

                return BadRequest(customErrorResponse);
            }
            try
            {
                var newUser = await _userService.Register(userDto);
                return Ok(newUser);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var customErrorResponse = new
                {
                    Title = "One or more validation errors occurred.",
                    Errors = errors
                };

                return BadRequest(customErrorResponse);
            }
            try
            {
                var token = await _userService.Login(userDto);
                return Ok(new { Message = "Login Successful", Token = token });
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<UserRegisterDto>> UpdateUser(UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var customErrorResponse = new
                {
                    Title = "One or more validation errors occurred.",
                    Errors = errors
                };

                return BadRequest(customErrorResponse);
            }
            try
            {
                var updatedUser = await _userService.UpdateUser(userDto, Guid.Parse(User.FindFirst("id").Value));
                return Ok(updatedUser);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteUser()
        {

            try
            {
                var result = await _userService.DeleteUser(Guid.Parse(User.FindFirst("id").Value));
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("profile/{userid}")]
        [Authorize]
        public async Task<ActionResult> GetUserProfile(Guid userid)
        {
            try
            {
                var user = await _userService.GetUserProfile(userid);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("recommended-jobs")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Job>>> GetRecommendedJobs(int pageNumber, int pageSize)
        {
            try
            {
                var jobs = await _userService.GetRecommendedJobs(pageNumber, pageSize, Guid.Parse(User.FindFirst("id").Value));
                return Ok(jobs);
            }
            catch (AreasOfInterestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (JobNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("jobmatch/{jobId}")]
        [Authorize]
        public async Task<IActionResult> GetJobMatchPercentage(Guid jobId)
        {
            try
            {
                var matchPercentage = await _userService.CalculateJobMatchPercentage(jobId, Guid.Parse(User.FindFirst("id").Value));

                var response = new
                {
                    jobId,
                    matchPercentage,
                    message = $"You have a {matchPercentage}% match with this job."
                };


                return Ok(response);
            }
            catch (UserNotFoundException)
            {
                return NotFound(new { message = "User not found." });
            }
            catch (JobNotFoundException)
            {
                return NotFound(new { message = "Job not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while calculating job match percentage.", error = ex.Message });
            }
        }

    }
}
