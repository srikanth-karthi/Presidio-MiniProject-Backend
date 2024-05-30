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

using System.Linq;
using Job_Portal_Application.Repository;
using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Dto.SkillDtos;


namespace Job_Portal_Application.Services.UsersServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        private readonly IJobRepository _jobRepository;
        private readonly IRepository<Guid, Credential> _credentialRepository;
        private readonly IUserSkillsRepository _userSkillsRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IRepository<Guid, Skill> _skillRepository;

        public UserService(IRepository<Guid, Skill> skillRepository, ICompanyRepository companyRepository, IUserSkillsRepository UserSkillsRepository,IRepository<Guid, Credential> CredentialRepository, IJobRepository jobRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _jobRepository = jobRepository;
            _credentialRepository= CredentialRepository;
            _userSkillsRepository = UserSkillsRepository;
            _companyRepository = companyRepository;
            _skillRepository = skillRepository;
        }

        public async Task<UserDto> Register(UserRegisterDto userDto)
        {

            var existingUser = await _companyRepository.GetByEmail(userDto.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User is already registered as a Company and cannot register as a User.");
            }

       

            if (await _userRepository.GetByEmail(userDto.Email) != null)
            {
                throw new UserAlreadyExistsException("User is already registered");
            }
            HMACSHA512 hmacSha = new HMACSHA512();


              var creadentials=    await  _credentialRepository.Add(new Credential()
                    {
                        
                        Password = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                        HasCode = hmacSha.Key,
                        Role=Roles.User,

                    });

     
            
                var newUser = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    CredentialId = creadentials.CredentialId,
                    Dob = DateOnly.FromDateTime(userDto.Dob),
                    Address = userDto.Address,
                    City = userDto.City,
                    PortfolioLink = userDto.PortfolioLink,
                    Phonenumber = userDto.Phonenumber,
                    ResumeUrl = userDto.ResumeUrl,
           
                };

           return  ToUserDto(await _userRepository.Add(newUser));
           
            
        }

        public async Task<string> Login(LoginDto userDto)
        {

            var user = await _userRepository.GetByEmail(userDto.Email) ?? throw new InvalidCredentialsException("Invalid Credentials");

          var credential= await  _credentialRepository.Get(user.CredentialId);


            using (HMACSHA512 hmacSha = new HMACSHA512(credential.HasCode))
            {
                var encryptedPass = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
                if (_tokenService.VerifyPassword(credential.Password, encryptedPass))
                {
                    var token = _tokenService.GenerateToken(user.UserId, credential.Role);
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
            Job job = await _jobRepository.Get(jobId) ?? throw new JobNotFoundException("Job not found");

            var jobSkills = job.JobSkills.Select(js => js.SkillId);
            var userSkills = userProfile.UserSkills.Select(us => us.SkillId);
            var matchingSkills = jobSkills.Intersect(userSkills).Count();
            var totalSkills = jobSkills.Count();
            var skillMatchPercentage = totalSkills == 0 ? 0 : (double)matchingSkills / totalSkills;

            var requiredExperience = job.ExperienceRequired ?? 0;
            var jobTitle = job.Title.TitleName;
            var userExperienceWithTitle = userProfile.Experiences
                .Where(e => e.TitleName==job.Title.TitleName)
                .Sum(e => e.ExperienceDuration);
            var experienceMatchPercentage = requiredExperience == 0 ? 0 : Math.Min(1, userExperienceWithTitle / requiredExperience);

            var jobAreaOfInterestId = job.Title.TitleName;
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



        public async Task<List<UserSkillDto>> GetSkills(Guid userId)
        {
            var user = await _userSkillsRepository.GetByUserId(userId) ?? throw new UserSkillsNotFoundException("User SKills not found.");

            return user.Select(skill => ToUserSkillDto(skill.Skill)).ToList();
        }

        private UserSkillDto ToUserSkillDto(Skill value)
        {
            return new UserSkillDto()
            {
                SkillId = value.SkillId,
                SkillName = value.Skill_Name
            };
        }

        public async Task<SkillsresponseDto> UserSkills(SkillsDto SkillsDto, Guid UserId)
        {
            SkillsresponseDto response = new();

            var job = await _userRepository.Get(UserId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist.");

            foreach (var skillId in SkillsDto.SkillsToAdd)
            {
                var existingJobSkill = await _userSkillsRepository.GetByUserIdAndSkillId(UserId, skillId);


                if (existingJobSkill == null)
                {
                    var skill = await _skillRepository.Get(skillId);
                    if (skill != null)
                    {

                        await _userSkillsRepository.Add(new UserSkills { UserId = UserId, SkillId = skillId });
                        response.AddedSkills.Add(skillId);
                    }
                    else
                    {
                        response.InvalidSkills.Add(skillId);
                    }
                }

            }


            foreach (var skillId in SkillsDto.SkillsToRemove)
            {
                var existingJobSkill = await _userSkillsRepository.GetByUserIdAndSkillId(UserId, skillId);


         
                    var skill = await _skillRepository.Get(skillId);
                if (skill != null && existingJobSkill != null)
                {

                        await _userSkillsRepository.Delete(existingJobSkill);
                        response.RemovedSkills.Add(skillId);
                    }
                    else
                    {
                        response.InvalidSkills.Add(skillId);
                    }
                

            }
            return response;

        }


        private JobDto MapToJobDto(Job job)
        {
            return new JobDto
            {
                JobId = job.JobId,
            JobType = job.JobType.ToString(),
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
