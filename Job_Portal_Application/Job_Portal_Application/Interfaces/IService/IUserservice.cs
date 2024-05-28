using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.profile;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IUserService
    {
        Task<UserDto> Register(UserRegisterDto userDto);
        Task<string> Login(LoginDto userDto);
        Task<IEnumerable<JobDto>> GetRecommendedJobs( int pageNumber, int pageSize);
        Task<UserDto> UpdateUser(UpdateUserDto userDto);
        Task<bool> DeleteUser();
        Task<UserDto> UpdateResumeUrl(Guid userId, string resumeUrl);


        Task<UserProfileDto> GetUserProfile(Guid userId);
    }
}
