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
using System.Diagnostics.CodeAnalysis;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/UserExperience")]
    [Authorize(Roles = "User")]
    [ExcludeFromCodeCoverage]
    public class UserExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;

        public UserExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpPost]

        public async Task<ActionResult<Experience>> AddExperience([FromBody] AddExperienceDto experienceDto)
        {
            try
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
                var addedExperience = await _experienceService.AddExperience(experienceDto, Guid.Parse(User.FindFirst("id").Value));
                return Ok(addedExperience);
            }
            catch (TitleNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidExperienceDateException ex)
            {
                return StatusCode(400, $"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]

        public async Task<ActionResult<Experience>> UpdateExperience(GetExperienceDto experienceDto)
        {
            try
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
                var updatedExperience = await _experienceService.UpdateExperience(experienceDto, Guid.Parse(User.FindFirst("id").Value));
                return Ok(updatedExperience);
            }
            catch (ExperienceNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidExperienceDateException ex)
            {
                return StatusCode(400, $"An error occurred: {ex.Message}");
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

        public async Task<ActionResult<bool>> DeleteExperience(Guid experienceId)
        {
            try
            {
                var result = await _experienceService.DeleteExperience(experienceId, Guid.Parse(User.FindFirst("id").Value));
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

        public async Task<ActionResult<GetExperienceDto>> GetExperience(Guid experienceId)
        {
            try
            {
                var experience = await _experienceService.GetExperience(experienceId, Guid.Parse(User.FindFirst("id").Value));
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

        public async Task<ActionResult<IEnumerable<GetExperienceDto>>> GetAllExperiences()
        {
            try
            {
                var experiences = await _experienceService.GetAllExperiences( Guid.Parse(User.FindFirst("id").Value));
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
