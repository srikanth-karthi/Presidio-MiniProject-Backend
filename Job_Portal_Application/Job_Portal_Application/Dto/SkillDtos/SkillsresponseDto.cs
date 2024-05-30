namespace Job_Portal_Application.Dto.SkillDtos
{
    public class SkillsresponseDto
    {

        public List<Guid> AddedSkills { get; set; } = new List<Guid>();
        public List<Guid> RemovedSkills { get; set; } = new List<Guid>();
        public List<Guid> InvalidSkills { get; set; } = new List<Guid>();

    }
}
