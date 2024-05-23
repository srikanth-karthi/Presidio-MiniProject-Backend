namespace Job_Portal_Application.Models
{
    public class JobSkills
    {

        public Guid JobSkillsId { get; set; } = Guid.NewGuid();

        public Guid JobId { get; set; }

        public Job Job { get; set; }

        public Guid SkillId { get; set; }

        public Skill Skill { get; set; }
    }
}
