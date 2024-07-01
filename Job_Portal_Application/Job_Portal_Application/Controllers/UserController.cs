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
using Job_Portal_Application.Services.CompanyService;
using Job_Portal_Application.Dto.SkillDtos;
using Azure;

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
        [HttpPost("upload-User-profilepicture")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadUserProfilePicture( IFormFile logo)
        {
            try
            {
                var logoUrl = await _userService.UploadUserProfilePicture(Guid.Parse(User.FindFirst("id").Value), logo);
                return Ok(new { logoUrl });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete-User-profilepicture")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteUserProfilePicture()
        {
            try
            {
                var result = await _userService.DeleteUserProfilePicture(Guid.Parse(User.FindFirst("id").Value));
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        public async Task<ActionResult<bool>> DeleteUser()
        {

            try
            {
                if(await _userService.DeleteUser(Guid.Parse(User.FindFirst("id").Value)))
                      return Ok("User Deleted Sucessfully");
                return BadRequest();
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
        [Authorize(Roles = "User,Company")]
        public async Task<ActionResult> GetUserProfilebyid(Guid userid)
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
        [HttpGet("profile")]
        [Authorize(Roles = "User,Company")]
        public async Task<ActionResult> GetUserProfile()
        {
            try
            {
                var user = await _userService.GetUserProfile(Guid.Parse(User.FindFirst("id").Value));
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
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
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
        [HttpPost("skills")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateuserSkills(SkillsDto SkillsDto)
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
                var userId = Guid.Parse(User.FindFirst("id").Value);
                var result = await _userService.UserSkills(SkillsDto, userId);


                var response = new
                {
                    Message = "Job skills updated successfully.",
                    result
                };

                return Ok(response);
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

        [HttpGet("GetSkills")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> GetSkills()
        {

            try
            {
                var skills = await _userService.GetSkills(Guid.Parse(User.FindFirst("id").Value));
                return Ok(skills);
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

    }
}
