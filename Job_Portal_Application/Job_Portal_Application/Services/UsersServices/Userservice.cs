using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Job_Portal_Application.Dto.profile;
using Job_Portal_Application.Dto;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.CompanyRepos;
using Job_Portal_Application.Repository.UserRepos;
using UserSkillDto = Job_Portal_Application.Dto.UserSkillDto;
using System.Linq;


namespace Job_Portal_Application.Services.UsersServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;

        public UserService(IJobRepository jobRepository, ICompanyRepository companyRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;


        }

        public async Task<UserDto> Register(UserRegisterDto userDto)
        {
            var existingUser = await _companyRepository.GetByEmail(userDto.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User is already registered as a Company and cannot register as a User.");
            }

            var user = await _userRepository.GetByEmail(userDto.Email);
            if (user != null)
            {
                throw new UserAlreadyExistsException($"{userDto.Email} already used, please try with another email.");
            }

            HMACSHA512 hmacSha = new HMACSHA512();
            
                var newUser = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Dob = DateOnly.FromDateTime(userDto.Dob),
                    Address = userDto.Address,
                    City = userDto.City,
                    PortfolioLink = userDto.PortfolioLink,
                    Phonenumber = userDto.Phonenumber,
                    ResumeUrl = userDto.ResumeUrl,
                    Password = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                    HasCode = hmacSha.Key
                };

           return  ToUserDto(await _userRepository.Add(newUser));
           
            
        }

        public async Task<string> Login(LoginDto userDto)
        {
            var user = await _userRepository.GetByEmail(userDto.Email);
            if (user == null)
            {
                throw new InvalidCredentialsException("Email not found.");
            }

            using (HMACSHA512 hmacSha = new HMACSHA512(user.HasCode))
            {
                var encryptedPass = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
                if (_tokenService.VerifyPassword(user.Password, encryptedPass))
                {
                    var token = _tokenService.GenerateToken(user.UserId);
                    return token;
                }
            }

            throw new InvalidCredentialsException("Invalid password.");
        }

        public async Task<IEnumerable<JobDto>> GetRecommendedJobs( int pageNumber, int pageSize, Guid UserId)
        {
            var jobs = await _userRepository.GetRecommendedJobsForUser(UserId, pageNumber, pageSize);
            if (!jobs.Any()) throw new JobNotFoundException(" Job does not exist.");

            return jobs.Select(j => MapToJobDto(j));
        }

        public async Task<UserDto> UpdateUser(UpdateUserDto userDto, Guid UserId)
        {
            var user = await _userRepository.Get(UserId) ?? throw new UserNotFoundException("User not found.");

            user.Name = userDto.Name;
            user.Address = userDto.Address;
            user.City = userDto.City;
            user.PortfolioLink = userDto.PortfolioLink;
            user.Phonenumber = userDto.PhoneNumber;
            user.ResumeUrl = userDto.ResumeUrl;

            return ToUserDto(await _userRepository.Update(user));

        }

        public async Task<bool> DeleteUser( Guid UserId)
        {
            var user = await _userRepository.Get(UserId) ?? throw new UserNotFoundException("User not found.");

            return await _userRepository.Delete(user);
        }

        public async Task<UserProfileDto> GetUserProfile(Guid userId)
        {
            var user = await _userRepository.GetUserProfile(userId) ?? throw new UserNotFoundException("User not found.");

         

            var userProfileDto = new UserProfileDto
            {
                UserId = user.UserId,
                Dob = user.Dob.ToDateTime(TimeOnly.MinValue),
                Email = user.Email,
                Name = user.Name,
                Address = user.Address,
                City = user.City,
                PortfolioLink = user.PortfolioLink,
                PhoneNumber = user.Phonenumber,
                ResumeUrl = user.ResumeUrl,
                Educations = user.Educations.Select(e => new EducationDto
                {
                    EducationId = e.EducationId,
                    Level = e.Level,
                    StartYear = e.StartYear,
                    EndYear = e.EndYear,
                    Percentage = e.Percentage,
                    InstitutionName = e.InstitutionName,
                    IsCurrentlyStudying = e.IsCurrentlyStudying
                }).ToList(),
                Experiences = user.Experiences.Select(e => new ExperienceDto
                {
                    ExperienceId = e.ExperienceId,
                    CompanyName = e.CompanyName,
                    TitleName = e.Title.TitleName,
                    StartYear = e.StartYear,
                    EndYear = e.EndYear,
                    ExperienceDuration = e.ExperienceDuration
                }).ToList(),
                UserSkills = user.UserSkills.Select(us => new Dto.profile.UserSkillDto
                {
                    SkillId = us.Skill.SkillId,
                    SkillName = us.Skill.Skill_Name
                }).ToList(),

                AreasOfInterests = user.AreasOfInterests.Select(aoi => new AreaOfInterestDto
                {
                    AreaOfInterestId = aoi.AreasOfInterestId,
                    TitleName = aoi.Title.TitleName,
                    Lpa = aoi.Lpa
                }).ToList()
            };

            return userProfileDto;
        }

        public  async Task<double> CalculateJobMatchPercentage(Guid jobId, Guid userId)
        {
            UserProfileDto userProfile = await GetUserProfile(userId);
            Job job= await _jobRepository.Get(jobId);

            var jobSkills = job.JobSkills.Select(js => js.SkillId);
            var userSkills = userProfile.UserSkills.Select(us => us.SkillId);
            var matchingSkills = jobSkills.Intersect(userSkills).Count();
            var totalSkills = jobSkills.Count();
            var skillMatchPercentage = totalSkills == 0 ? 0 : (double)matchingSkills / totalSkills;

            var requiredExperience = job.ExperienceRequired ?? 0;
            var jobTitle = job.Title.TitleName;
            var userExperienceWithTitle = userProfile.Experiences
                .Where(e => e.TitleName.Equals(jobTitle, StringComparison.OrdinalIgnoreCase))
                .Sum(e => e.ExperienceDuration);
            var experienceMatchPercentage = requiredExperience == 0 ? 0 : Math.Min(1, userExperienceWithTitle / requiredExperience);

            var jobAreaOfInterestId = job.TitleId.ToString();
            var userAreaOfInterestIds = userProfile.AreasOfInterests.Select(aoi => aoi.TitleName);
            var areaOfInterestMatchPercentage = userAreaOfInterestIds.Contains(jobAreaOfInterestId) ? 1 : 0;

            var totalWeightedPercentage = (skillMatchPercentage * 0.35) +
                                          (experienceMatchPercentage * 0.35) +
                                          (areaOfInterestMatchPercentage * 0.30);

            return totalWeightedPercentage * 100;
        }


        public async Task<UserDto> UpdateResumeUrl(Guid userId, string resumeUrl)
        {
            var user = await _userRepository.Get(userId) ?? throw new UserNotFoundException("User not found.");

            user.ResumeUrl = resumeUrl;
            return ToUserDto(await _userRepository.Update(user));
        }


        private JobDto MapToJobDto(Job job)
        {
            return new JobDto
            {
                JobId = job.JobId,

                JobType = Enum.GetName(typeof(JobType), job.JobType),
                TitleId = job.TitleId,
                CompanyName = job.Company.CompanyName,
                DatePosted = job.DatePosted.ToDateTime(TimeOnly.MinValue),
                TitleName = job.Title?.TitleName,
                Status = job.Status,
                ExperienceRequired = job.ExperienceRequired,
                Lpa = job.Lpa,
                JobDescription = job.JobDescription,
                Skills = job.JobSkills.Select(js => js.Skill.Skill_Name).ToList()
            };
        }

        public  UserDto ToUserDto( User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Dob = user.Dob.ToDateTime(TimeOnly.MinValue),
                Email = user.Email,
                Name = user.Name,
                Address = user.Address,
                City = user.City,
                PortfolioLink = user.PortfolioLink,
                PhoneNumber = user.Phonenumber,
                ResumeUrl = user.ResumeUrl
            };
        }

    }
}
