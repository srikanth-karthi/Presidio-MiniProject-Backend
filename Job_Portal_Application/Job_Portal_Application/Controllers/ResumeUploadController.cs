using Job_Portal_Application.Dto.JobActivityDto;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Job_Portal_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;
        private readonly IUserService _userService;
        private readonly IJobActivityService _jobActivityService;

        public ResumeController(IUserService userService, IAuthorizeService authorizeService, IJobActivityService jobActivityService)
        {
            _authorizeService = authorizeService;
            _userService = userService;
            _jobActivityService = jobActivityService;
        }
        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        [Authorize]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".pdf")
                return BadRequest("Invalid file type. Only PDF files are allowed.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            var uniqueFileName = $"{_authorizeService.Gettoken()}.pdf";
            var filePath = Path.Combine(folderPath, uniqueFileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            var resumeUrl = $"{Request.Scheme}://{Request.Host}/api/resume/view/{_authorizeService.Gettoken()}";
            var updatedUser = await _userService.UpdateResumeUrl(_authorizeService.Gettoken(), resumeUrl);

            return Ok(new { message = "Resume uploaded successfully.", resumeUrl = updatedUser.ResumeUrl });
        }

        [HttpGet("download/{userId}")]
        [Authorize]
        public async Task<IActionResult> Download(Guid userId)
        {
            string originalFileName = $"{userId}.pdf";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            var originalFilePath = Path.Combine(folderPath, originalFileName);

            if (!System.IO.File.Exists(originalFilePath))
            {
                return NotFound($"The file '{originalFileName}' was not found.");
            }

            var user = await _userService.GetUserProfile(userId);
            var tempFileName = $"{user.Name}-resume.pdf";
            var tempFilePath = Path.Combine(folderPath, tempFileName);

            using (var inputStream = new FileStream(originalFilePath, FileMode.Open))
            using (var outputStream = new FileStream(tempFilePath, FileMode.Create))
            {
                await inputStream.CopyToAsync(outputStream);
            }

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(tempFilePath);
            return File(fileBytes, "application/pdf", tempFileName);
        }

        [HttpGet("view/{userId}/{jobactivityId}")]
        [Authorize]
        public async Task<IActionResult> View(Guid userId, Guid jobactivityId)
        {
            string originalFileName = $"{userId}.pdf";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            var originalFilePath = Path.Combine(folderPath, originalFileName);

            if (!System.IO.File.Exists(originalFilePath))
            {
                return NotFound($"The file '{originalFileName}' was not found.");
            }

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(originalFilePath);

            await _jobActivityService.UpdateJobActivityStatus(new UpdateJobactivityDto()
            {
                JobactivityId = jobactivityId,
                ResumeViewed = true
            });

            return File(fileBytes, "application/pdf");
        }

        [HttpGet("view/{userId}")]
        [Authorize]
        public async Task<IActionResult> View(Guid userId)
        {
            string originalFileName = $"{userId}.pdf";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            var originalFilePath = Path.Combine(folderPath, originalFileName);

            if (!System.IO.File.Exists(originalFilePath))
            {
                return NotFound($"The file '{originalFileName}' was not found.");
            }

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(originalFilePath);
            return File(fileBytes, "application/pdf");
        }
    }
}
