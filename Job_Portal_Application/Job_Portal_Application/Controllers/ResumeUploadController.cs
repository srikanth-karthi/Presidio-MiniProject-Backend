using Job_Portal_Application.Dto.JobActivityDto;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJobActivityService _jobActivityService;
        private readonly MinIOService _minioService;

        public ResumeController(IUserService userService, IJobActivityService jobActivityService, MinIOService minioService)
        {
            _userService = userService;
            _jobActivityService = jobActivityService;
            _minioService = minioService;
        }

        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".pdf")
                return BadRequest("Invalid file type. Only PDF files are allowed.");

            var uniqueFileName = $"{Guid.Parse(User.FindFirst("id").Value)}.pdf";

            using (var stream = file.OpenReadStream())
            {
                await _minioService.UploadFileAsync(uniqueFileName, stream);
            }

            var resumeUrl = $"{Request.Scheme}://{Request.Host}/api/resume/view/{Guid.Parse(User.FindFirst("id").Value)}";
            var updatedUser = await _userService.UpdateResumeUrl(Guid.Parse(User.FindFirst("id").Value), resumeUrl);

            return Ok(new { message = "Resume uploaded successfully.", resumeUrl = updatedUser.ResumeUrl });
        }

        [HttpGet("download/{userId}")]
        [Authorize(Roles = "User,Company")]
        public async Task<IActionResult> Download(Guid userId)
        {
            var fileKey = $"{userId}.pdf";
            Stream fileStream;

            try
            {
                fileStream = await _minioService.DownloadFileAsync(fileKey);
            }
            catch (Exception ex)
            {
                return NotFound($"The file was not found: {ex.Message}");
            }

            var user = await _userService.GetUserProfile(userId);
            var tempFileName = $"{user.Name}-resume.pdf";

            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                fileStream.Close();
                return File(memoryStream.ToArray(), "application/pdf", tempFileName);
            }
        }

        [HttpGet("view/{userId}/{jobactivityId}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> View(Guid userId, Guid jobactivityId)
        {
            var fileKey = $"{userId}.pdf";
            Stream fileStream;

            try
            {
                fileStream = await _minioService.DownloadFileAsync(fileKey);
            }
            catch (Exception ex)
            {
                return NotFound($"The file was not found: {ex.Message}");
            }

            byte[] fileBytes;

            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
                fileStream.Close();
            }
           
            await _jobActivityService.Updateresumestatus( jobactivityId);

            return File(fileBytes, "application/pdf");
        }

        [HttpGet("view/{userId}")]
        public async Task<IActionResult> View(Guid userId, [FromQuery] string token)
        {
            if (!ValidateToken(token))
            {
                return Unauthorized("Invalid token.");
            }

            var fileKey = $"{userId}.pdf";
            Stream fileStream;

            try
            {
                fileStream = await _minioService.DownloadFileAsync(fileKey);
            }
            catch (Exception ex)
            {
                return NotFound($"The file was not found: {ex.Message}");
            }

            byte[] fileBytes;

            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
                fileStream.Close();
            }

            return File(fileBytes, "application/pdf");
        }

        private bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("y_J82VYvg5Jh8-DT89E1kz_FzHNN3tB_Sy4b_yLhoZ8Y6q-jVOWU3-xPFlPS6cYYicWlb0XhREXAf3OWTbnN3w=="); 
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var role = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value;

                // Check if the role is either "Company" or "User"
                return role == "Company" || role == "User";
            }
            catch
            {
                return false;
            }
        }
    }
}
