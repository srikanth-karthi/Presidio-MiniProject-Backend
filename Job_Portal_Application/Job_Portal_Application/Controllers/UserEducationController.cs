using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Dto.EducationDtos;
using Job_Portal_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Job_Portal_Application.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;
using System.Diagnostics.CodeAnalysis;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/UserEducation")]
    [Authorize(Roles = "User")]
    [ExcludeFromCodeCoverage]
    public class UserEducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public UserEducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpPost]

        public async Task<ActionResult<EducationDto>> AddEducation(AddEducationDto educationDto)
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
                var addedEducation = await _educationService.AddEducation(educationDto, Guid.Parse(User.FindFirst("id").Value));
                return Ok(addedEducation);
            }
            catch (InvalidEducationDateException ex)
            {
                return StatusCode(400, $"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]

        public async Task<ActionResult<EducationDto>> UpdateEducation(EducationDto educationDto)
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
                var updatedEducation = await _educationService.UpdateEducation(educationDto, Guid.Parse(User.FindFirst("id").Value));
                return Ok(updatedEducation);
            }
            catch (EducationNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidEducationDateException ex)
            {
                return StatusCode(400, $"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{educationId}")]

        public async Task<ActionResult<bool>> DeleteEducation(Guid educationId)
        {
            try
            {
                var result = await _educationService.DeleteEducation(educationId, Guid.Parse(User.FindFirst("id").Value));
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

        public async Task<ActionResult<EducationDto>> GetEducation(Guid educationId)
        {
            try
            {
                var education = await _educationService.GetEducation(educationId, Guid.Parse(User.FindFirst("id").Value));
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

        public async Task<ActionResult<IEnumerable<EducationDto>>> GetAllEducations()
        {
            try
            {
                var educations = await _educationService.GetAllEducations( Guid.Parse(User.FindFirst("id").Value));
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
