using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobActivityDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    [ExcludeFromCodeCoverage]
    public class JobActivityController : ControllerBase
    {
        private readonly IJobActivityService _jobActivityService;

        public JobActivityController(IJobActivityService jobActivityService)
        {
            _jobActivityService = jobActivityService;
        }

        [HttpPost("apply")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ApplyForJob(Guid jobId)
        {
            try
            {
                var jobActivityDto = await _jobActivityService.ApplyForJob(jobId, Guid.Parse(User.FindFirst("id").Value));
                return Ok(jobActivityDto);
            }
            catch (JobNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (JobDisabledException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (JobAlreadyAppliedException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("filtered")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetFilteredJobActivities(Guid jobId, int pageNumber = 1, int pageSize = 25, bool firstApplied = false, bool perfectMatchSkills = false, bool perfectMatchExperience = false, bool hasExperienceInJobTitle = false)
        {
            try
            {
                var jobActivities = await _jobActivityService.GetFilteredUser(Guid.Parse(User.FindFirst("id").Value),jobId, pageNumber, pageSize, firstApplied, perfectMatchSkills, perfectMatchExperience, hasExperienceInJobTitle);
                return Ok(jobActivities);
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

        [HttpGet("user/appliedjobs")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetJobsUserApplied()
        {
            try
            {
                var jobActivities = await _jobActivityService.GetJobsUserApplied(Guid.Parse(User.FindFirst("id").Value));
                return Ok(jobActivities);
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

        [HttpPut("Update")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UpdateJobActivityStatus(UpdateJobactivityDto updateJobactivityDto)
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
                var jobActivityDto = await _jobActivityService.UpdateJobActivityStatus(updateJobactivityDto);
                return Ok(jobActivityDto);
            }
            catch (JobActivityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{jobActivityId}")]
        [Authorize(Roles = "Company,User")]
        public async Task<IActionResult> GetJobActivityById(Guid jobActivityId)
        {
            try
            {
                var jobActivityDto = await _jobActivityService.GetJobActivityById(jobActivityId);
                return Ok(jobActivityDto);
            }
            catch (JobActivityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("job/{jobId}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetJobActivitiesByJobId(Guid jobId)
        {
            try
            {
                var jobActivityDtos = await _jobActivityService.GetJobActivitiesByJobId(jobId);
                return Ok(jobActivityDtos);
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
