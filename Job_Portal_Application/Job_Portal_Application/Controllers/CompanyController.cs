using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Dto.CompanyDto;
using Job_Portal_Application.Dto.CompanyDtos;
using Job_Portal_Application.Dto.UserDto;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExcludeFromCodeCoverage]
   
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpPost("upload-logo")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UploadLogo(IFormFile logo)
        {
            var LogoUrl = await _companyService.UploadCompanyLogo(Guid.Parse(User.FindFirst("id").Value), logo);
            return Ok(new { message = "Logo uploaded successfully.", logoUrl = LogoUrl });
        }

        [HttpDelete("delete-logo")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> DeleteLogo()
        {
            var result = await _companyService.DeleteCompanyLogo(Guid.Parse(User.FindFirst("id").Value));
            return Ok(new { message = "Logo deleted successfully.", success = result });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterCompany([FromBody] CompanyRegisterDto companyDto)
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
                var newCompany = await _companyService.Register(companyDto);
                return CreatedAtAction(nameof(GetCompany), new { id = newCompany.CompanyId }, newCompany);
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
        public async Task<IActionResult> Login([FromBody] LoginDto companyDto)
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
                var token = await _companyService.Login(companyDto);
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

        [HttpPut]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyUpdateDto company)
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
                var updatedCompany = await _companyService.UpdateCompany(company, Guid.Parse(User.FindFirst("id").Value));
                return Ok(updatedCompany);
            }
            catch (CompanyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> DeleteCompany()
        {

            try
            {
                var success = await _companyService.DeleteCompany( Guid.Parse(User.FindFirst("id").Value));
                if (success)
                {
                    return Ok(new { message = "Successfully deleted the Company" });
                }
                return BadRequest(new { message = "Failed to delete company." });
            }
            catch (CompanyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            try
            {
                var company = await _companyService.GetCompany(id);
                return Ok(company);
            }
            catch (CompanyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("profile")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                var company = await _companyService.GetCompany(Guid.Parse(User.FindFirst("id").Value));
                return Ok(company);
            }
            catch (CompanyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet]

        [Authorize(Roles = "User,Company")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await _companyService.GetAllCompanies();
                return Ok(companies);
            }
            catch (CompanyNotFoundException ex)
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
