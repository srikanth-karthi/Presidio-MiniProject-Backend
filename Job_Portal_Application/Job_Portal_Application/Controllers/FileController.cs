
    using Job_Portal_Application.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
namespace Job_Portal_Application.Controllers
{
    public class FileController : Controller
    {
        private readonly MinIOService _minIOService;

        public FileController(MinIOService minIOService)
        {
            _minIOService = minIOService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var stream = file.OpenReadStream())
            {
                await _minIOService.UploadFileAsync(file.FileName, stream);
            }

            return Ok("File uploaded successfully.");
        }


        [HttpGet("download/{key}")]
        public async Task<IActionResult> DownloadFile(string key)
        {
            var fileStream = await _minIOService.DownloadFileAsync(key);
            return File(fileStream, "application/octet-stream", key);
        }

        [HttpDelete("delete/{key}")]
        public async Task<IActionResult> DeleteFile(string key)
        {
            await _minIOService.DeleteFileAsync(key);
            return Ok("File deleted successfully.");
        }
    }

}
