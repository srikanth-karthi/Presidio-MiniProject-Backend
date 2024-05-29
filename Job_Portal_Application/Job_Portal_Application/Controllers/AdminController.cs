﻿using Microsoft.AspNetCore.Mvc;
using Job_Portal_Application.Models;
using Job_Portal_Application.Services;
using System;
using System.Threading.Tasks;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Services.UsersServices;
using Microsoft.AspNetCore.Authorization;
using Job_Portal_Application.Interfaces.IRepository;
using System.Diagnostics.CodeAnalysis;

namespace Job_Portal_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserRepository _userRepository;

        public AdminController(IAdminService adminService, IUserRepository userRepository)
        {
            _adminService = adminService;
            _userRepository = userRepository;
        }

        private async Task<bool> AuthorizeAdmin()
        {
            var userId = Guid.Parse(User.FindFirst("id").Value);
            var user = await _userRepository.Get(userId);
            if (user == null || user.Email != "Admin@jobportal.com")
            {
                return false;
            }
            return true;
        }

        // Skill Endpoints
        [HttpPost("skills")]
        public async Task<IActionResult> CreateSkill([FromBody] string SkillName)
        {
            if (!await AuthorizeAdmin())
                return Unauthorized(new { message = "Not authorized as admin." });

            try
            {
                var createdSkill = await _adminService.CreateSkill(new Skill { Skill_Name = SkillName });
                return CreatedAtAction(nameof(CreateSkill), new { id = createdSkill.SkillId }, createdSkill);
            }
            catch (SkillAlreadyExisitException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("skills/{id}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            if (!await AuthorizeAdmin())
                return Unauthorized(new { message = "Not authorized as admin." });

            try
            {
                var result = await _adminService.DeleteSkill(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (SkillNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Title Endpoints
        [HttpPost("titles")]
        public async Task<IActionResult> CreateTitle([FromBody] string titleName)
        {
            if (!await AuthorizeAdmin())
                return Unauthorized(new { message = "Not authorized as admin." });

            try
            {
                var createdTitle = await _adminService.CreateTitle(new Title { TitleName = titleName });
                return CreatedAtAction(nameof(CreateTitle), new { id = createdTitle.TitleId }, createdTitle);
            }
            catch (TitleAlreadyExisitException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("titles/{id}")]
        public async Task<IActionResult> DeleteTitle(Guid id)
        {
            if (!await AuthorizeAdmin())
                return Unauthorized(new { message = "Not authorized as admin." });

            try
            {
                var result = await _adminService.DeleteTitle(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (TitleNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
