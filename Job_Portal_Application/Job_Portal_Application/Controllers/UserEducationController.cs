using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Dto.EducationDtos;
using Job_Portal_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Job_Portal_Application.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/UserEducation")]
    public class UserEducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public UserEducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EducationDto>> AddEducation(AddEducationDto educationDto)
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

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<EducationDto>> UpdateEducation(EducationDto educationDto)
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

        [HttpDelete("{educationId}")]
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

        [HttpGet("{educationId}")]
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EducationDto>>> GetAllEducations()
        {
            try
            {
                var educations = await _educationService.GetAllEducations();
                return Ok(educations);
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
    }
}
