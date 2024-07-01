using System;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Models
{
    public class Skill
    {
        [Key]
        public Guid SkillId { get; set; } = Guid.NewGuid();

 
        public string SkillName { get; set; }
    }
}
