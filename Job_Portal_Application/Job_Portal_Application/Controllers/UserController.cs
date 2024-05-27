using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Models;
using Job_Portal_Application.Exceptions;
using System;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.EducationDto;
using Job_Portal_Application.Dto.ExperienceDto;
using Job_Portal_Application.Dto.AreasOfInterestDto;
using Microsoft.AspNetCore.Authorization;
using Job_Portal_Application.Interfaces.IService;
using System.Collections.Generic;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Dto;
using Job_Portal_Application.Services;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEducationService _educationService;
        private readonly IExperienceService _experienceService;
        private readonly IAreasOfInterestService _areasOfInterestService;
        private readonly IUserSkillsService _userSkillsService;

        public UserController(IUserService userService, IEducationService educationService, IExperienceService experienceService, IAreasOfInterestService areasOfInterestService, IUserSkillsService userSkillsService)
        {
            _userService = userService;
            _educationService = educationService;
            _experienceService = experienceService;
            _areasOfInterestService = areasOfInterestService;
            _userSkillsService = userSkillsService;
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
        public async Task<ActionResult<UserRegisterDto>> UpdateUser(UserDto userDto)
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
        public async Task<ActionResult<User>> GetUserProfile(Guid userid)
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
              
                var jobs = await _userService.GetRecommendedJobs( pageNumber, pageSize);
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

        #region Education Endpoints

        [HttpPost("education")]
        [Authorize]
        public async Task<ActionResult<Education>> AddEducation(AddEducationDto educationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var addedEducation = await _educationService.AddEducation(educationDto);
                return Ok(addedEducation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("education")]
        [Authorize]
        public async Task<ActionResult<Education>> UpdateEducation(EducationDto educationDto)
        {
            try
            {
                var updatedEducation = await _educationService.UpdateEducation(educationDto);
                return Ok(updatedEducation);
            }
            catch (EducationNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("education/{educationId}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteEducation(Guid educationId)
        {
            try
            {
                var result = await _educationService.DeleteEducation(educationId);
                if (result)
                    return Ok(new { message = "Successfully deleted the education" });

                return StatusCode(500, new { message = "Error deleting the education" });
            }
            catch (EducationNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("education/{educationId}")]
        [Authorize]
        public async Task<ActionResult<EducationDto>> GetEducation(Guid educationId)
        {
            try
            {
                var education = await _educationService.GetEducation(educationId);
                return Ok(education);
            }
            catch (EducationNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("education")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EducationDto>>> GetAllEducations()
        {
            try
            {
                var educations = await _educationService.GetAllEducations();
                return Ok(educations);
            }

            catch(EducationNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        #endregion

        #region Experience Endpoints

        [HttpPost("experience")]
        [Authorize]
        public async Task<ActionResult<Experience>> AddExperience([FromBody] AddExperienceDto experienceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var addedExperience = await _experienceService.AddExperience(experienceDto);
                return Ok(addedExperience);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("experience")]
        [Authorize]
        public async Task<ActionResult<Experience>> UpdateExperience(ExperienceDto experienceDto)
        {
            try
            {
                var updatedExperience = await _experienceService.UpdateExperience(experienceDto);
                return Ok(updatedExperience);
            }
            catch (ExperienceNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("experience/{experienceId}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteExperience(Guid experienceId)
        {
            try
            {
                var result = await _experienceService.DeleteExperience(experienceId);
                if (result)
                    return Ok(new { message = "Successfully deleted the experience" });

                return StatusCode(500, new { message = "Error deleting the experience" });
            }
            catch (ExperienceNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("experience/{experienceId}")]
        [Authorize]
        public async Task<ActionResult<ExperienceDto>> GetExperience(Guid experienceId)
        {
            try
            {
                var experience = await _experienceService.GetExperience(experienceId);
                return Ok(experience);
            }
            catch (ExperienceNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("experience")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ExperienceDto>>> GetAllExperiences()
        {
            try
            {
                var experiences = await _experienceService.GetAllExperiences();
                return Ok(experiences);
            }
            catch (ExperienceNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        #endregion

        #region AreasOfInterest Endpoints

        [HttpPost("areasofinterest")]
        [Authorize]
        public async Task<ActionResult<AreasOfInterest>> AddAreasOfInterest(AddAreasOfInterestDTO areasOfInterestDto)
        {
            try
            {
                var addedAreasOfInterest = await _areasOfInterestService.AddAreasOfInterest(areasOfInterestDto);
                return Ok(addedAreasOfInterest);
            }
            catch (AreasOfInterestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("areasofinterest")]
        [Authorize]
        public async Task<ActionResult<AreasOfInterest>> UpdateAreasOfInterest(AreasOfInterestDto areasOfInterestDto)
        {
            try
            {
                var updatedAreasOfInterest = await _areasOfInterestService.UpdateAreasOfInterest(areasOfInterestDto);
                return Ok(updatedAreasOfInterest);
            }
            catch (AreasOfInterestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("areasofinterest/{areasOfInterestId}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteAreasOfInterest(Guid areasOfInterestId)
        {
            try
            {
                var result = await _areasOfInterestService.DeleteAreasOfInterest(areasOfInterestId);
                if (result)
                    return Ok(new { message = "Successfully deleted the AreaOfInterest" });

                return StatusCode(500, new { message = "Error deleting the AreaOfInterest" });
            }
            catch (AreasOfInterestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("areasofinterest/{areasOfInterestId}")]
        [Authorize]
        public async Task<ActionResult<AreasOfInterestDto>> GetAreasOfInterest(Guid areasOfInterestId)
        {
            try
            {
                var areasOfInterest = await _areasOfInterestService.GetAreasOfInterest(areasOfInterestId);
                return Ok(areasOfInterest);
            }
            catch (AreasOfInterestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("areasofinterest")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AreasOfInterestDto>>> GetAllAreasOfInterest()
        {
            try
            {
                var areasOfInterest = await _areasOfInterestService.GetAllAreasOfInterest();
                return Ok(areasOfInterest);
            }
            catch (AreasOfInterestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        #endregion

        #region UserSkills Endpoints

        [HttpPost("skills")]
        [Authorize]
        public async Task<ActionResult<UserSkillsResponseDto>> AddUserSkills([FromBody] UserSkillsRequestDto request)
        {
            try
            {
                var response = await _userSkillsService.AddUserSkills(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpDelete("skills")]
        [Authorize]
        public async Task<ActionResult<UserSkillsResponseDto>> RemoveUserSkill([FromBody] UserSkillsRequestDto request)
        {
            try
            {
                var response = await _userSkillsService.RemoveUserSkill(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet("skills")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserSkillDto>>> GetAllUserSkills()
        {
            try
            {
                var userSkills = await _userSkillsService.GetAllUserSkills();
                return Ok(userSkills);
            }
            catch (UserSkillsNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        #endregion
    }
}
