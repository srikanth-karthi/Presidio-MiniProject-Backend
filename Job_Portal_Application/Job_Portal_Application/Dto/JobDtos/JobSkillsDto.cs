namespace Job_Portal_Application.Dto.JobDto
{
    public class JobSkillsDto
    {
        public Guid JobId { get; set; }
        public List<Guid> SkillsToAdd { get; set; }
        public List<Guid> SkillsToRemove { get; set; }
    }

}
