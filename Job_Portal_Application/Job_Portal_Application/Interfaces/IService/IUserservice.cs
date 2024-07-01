using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.profile;
using Job_Portal_Application.Dto.SkillDtos;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<UserDto> Register(UserRegisterDto userDto);
        Task<string> Login(LoginDto userDto);
        Task<IEnumerable<JobDto>> GetRecommendedJobs( int pageNumber, int pageSize, Guid UserId);
        Task<UserDto> UpdateUser(UpdateUserDto userDto, Guid UserId);
        Task<bool> DeleteUser(Guid UserId);
        Task<UserDto> UpdateResumeUrl(Guid userId, string resumeUrl);
        Task<bool> DeleteUserProfilePicture(Guid userId);
        Task<string> UploadUserProfilePicture(Guid userId, IFormFile profilePicture);
        Task<double> CalculateJobMatchPercentage(Guid jobId, Guid userId);
        Task<UserProfileDto> GetUserProfile(Guid userId);
        Task<List<UserSkillDto>> GetSkills(Guid userId);
        Task<SkillsresponseDto> UserSkills(SkillsDto SkillsDto, Guid UserId);
    }
}
