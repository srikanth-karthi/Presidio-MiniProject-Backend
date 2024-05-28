using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Models;
using Job_Portal_Application.Exceptions;
using System;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.UserDto;
using Microsoft.AspNetCore.Authorization;
using Job_Portal_Application.Interfaces.IService;
using System.Collections.Generic;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/User")]
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
            try
            {
                var updatedUser = await _userService.UpdateUser(userDto);
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
                var result = await _userService.DeleteUser();
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
                var jobs = await _userService.GetRecommendedJobs(pageNumber, pageSize);
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
    }
}
