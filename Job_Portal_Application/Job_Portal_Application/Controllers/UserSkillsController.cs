using Microsoft.AspNetCore.Mvc;

using Job_Portal_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Job_Portal_Application.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Models;
using Job_Portal_Application.Dto;
using Job_Portal_Application.Services;

namespace Job_Portal_Application.Controllers
{
    [ApiController]
    [Route("api/UserSkills")]
    public class UserSkillsController : ControllerBase
    {
        private readonly IUserSkillsService _userSkillsService;

        public UserSkillsController(IUserSkillsService userSkillsService)
        {
            _userSkillsService = userSkillsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserSkillsResponseDto>> AddUserSkills([FromBody] UserSkillsRequestDto request)
        {
            try
            {
                var response = await _userSkillsService.AddUserSkills(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<UserSkillsResponseDto>> RemoveUserSkill([FromBody] UserSkillsRequestDto request)
        {
            try
            {
                var response = await _userSkillsService.RemoveUserSkill(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet("skills")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserSkillDto>>> GetAllUserSkills()
        {
            try
            {
                var userSkills = await _userSkillsService.GetAllUserSkills();
                return Ok(userSkills);
            }
            catch (UserSkillsNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}
