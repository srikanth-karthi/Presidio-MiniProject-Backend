namespace Job_Portal_Application.Models
{
    public class AreasOfInterest
    {
        public Guid AreasOfInterestId { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public Guid TitleId { get; set; }
        public Title Title { get; set; }
        public float Lpa { get; set; }
    }
}
