using System;
using System.Collections.Generic;

namespace Job_Portal_Application.Dto
{
    public class UserSkillsResponseDto
    {
        public List<UserSkillDto> Skills { get; set; } = new List<UserSkillDto>();
        public List<Guid> InvalidSkills { get; set; } = new List<Guid>();
    }
}
