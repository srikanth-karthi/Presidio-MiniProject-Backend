namespace Job_Portal_Application.Models
{
    public class UserSkills
    {

        public Guid UserSkillsId { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public Guid SkillId { get; set; }

        public Skill Skill { get; set; }
        public User User { get;  set; }
    }
}
