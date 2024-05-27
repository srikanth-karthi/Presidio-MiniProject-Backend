using System;

namespace Job_Portal_Application.Dto
{
    public class UserSkillDto
    {
        public Guid UserSkillsId { get; set; }
        public Guid UserId { get; set; }
        public Guid SkillId { get; set; }
        public string SkillName { get; set; }
    }
}
