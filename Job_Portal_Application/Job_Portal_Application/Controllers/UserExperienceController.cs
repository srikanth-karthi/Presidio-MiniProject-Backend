using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Dto.ExperienceDtos;
using Job_Portal_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Job_Portal_Application.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Job_Portal_Application.Models;
using Job_Portal_Application.Dto.UserDto;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/UserExperience")]
    public class UserExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;

        public UserExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpPost]
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
            catch (TitleNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Experience>> UpdateExperience(GetExperienceDto experienceDto)
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
            catch (TitleNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{experienceId}")]
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

        [HttpGet("{experienceId}")]
        [Authorize]
        public async Task<ActionResult<GetExperienceDto>> GetExperience(Guid experienceId)
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetExperienceDto>>> GetAllExperiences()
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
    }
}
