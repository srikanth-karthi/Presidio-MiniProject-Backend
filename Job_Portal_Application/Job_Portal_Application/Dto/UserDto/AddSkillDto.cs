using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.UserDto
{
    public class AddSkillsDTO
    {

        [Required(ErrorMessage = "At least one SkillId is required")]
        public List<Guid> SkillIds { get; set; }
    }
}
