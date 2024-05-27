using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IUserRepository : IRepository<Guid, User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetUserProfile(Guid id);
        Task<JobActivity> GetJob(Guid id);
        Task<IEnumerable<JobActivity>> GetAllJobs(Guid userId);
        public Task<IEnumerable<Job>> GetRecommendedJobsForUser(Guid userId, int pageNumber, int pageSize);

    }
}
