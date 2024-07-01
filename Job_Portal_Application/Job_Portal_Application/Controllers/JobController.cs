using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.JobDtos;
using Job_Portal_Application.Services.CompanyService;
using System.Diagnostics.CodeAnalysis;
using Job_Portal_Application.Dto.SkillDtos;
using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExcludeFromCodeCoverage]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }
        [Authorize(Roles = "Company")]

        [HttpPost("add")]

        public async Task<IActionResult> AddJob([FromBody] PostJobDto newJob)
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
                var (addedJob, notFoundSkills) = await _jobService.AddJob(newJob, Guid.Parse(User.FindFirst("id").Value));

                if (notFoundSkills.Count > 0)
                {
                    return CreatedAtAction(nameof(GetJob), new { id = addedJob.JobId }, new
                    {
                        Job = addedJob,
                        Message = "Job created with some skills not found.",
                        NotFoundSkills = notFoundSkills
                    });
                }

                return CreatedAtAction(nameof(GetJob), new { id = addedJob.JobId }, addedJob);
            }
            catch (CompanyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (SkillNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        [Authorize(Roles = "Company")]

        [HttpPut("update")]
        public async Task<IActionResult> UpdateJob([FromBody] UpdateJobDto jobUpdateDto)
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
                var updatedJob = await _jobService.UpdateJob(jobUpdateDto, Guid.Parse(User.FindFirst("id").Value));
                return Ok(updatedJob);
            }
            catch (JobNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        [Authorize(Roles = "Company")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            try
            {
                var success = await _jobService.DeleteJob(id,Guid.Parse(User.FindFirst("id").Value));
                if (success)
                {
                    return Ok(new { message = "Successfully deleted the job." });
                }
                return BadRequest(new { message = "Failed to delete job." });
            }
            catch (JobNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        [Authorize(Roles = "User,Company")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(Guid id)
        {
            try
            {
                var job = await _jobService.GetJob(id, Guid.Parse(User.FindFirst("id").Value));
                return Ok(job);
            }
            catch (JobNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetAllJobs()
        {
            try
            {
                var jobs = await _jobService.GetAllJobs(Guid.Parse(User.FindFirst("id").Value));
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost("search")]
        [Authorize(Roles = "User,Company")]
        public async Task<IActionResult> GetJobs([FromBody] JobsSearchRequest request)
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
                var jobs = await _jobService.GetJobs(
     Guid.Parse(User.FindFirst("id").Value),
     request.PageNumber,
     request.PageSize,
     request.JobTitleId,
     request.MinLpa,
     request.MaxLpa,
     request.RecentlyPosted ?? true,
     request.SkillIds,
     request.MinExperience,
     request.MaxExperience,
     request.Location,
     request.CompanyId,
     request.JobType);

                return Ok(jobs);
            }
            catch (JobNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPut("jobskills")]
        [Authorize(Roles = "Company")]

        public async Task<IActionResult> UpdateJobSkills(JobSkillsdto jobSkillsDto)
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
              
                var result = await _jobService.UpdateJobSkills(jobSkillsDto, Guid.Parse(User.FindFirst("id").Value));


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

    }
}
