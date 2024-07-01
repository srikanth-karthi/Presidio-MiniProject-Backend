using Job_Portal_Application.Dto.EducationDtos;
using Job_Portal_Application.Dto.ExperienceDtos;
using Job_Portal_Application.Dto.profile;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application.Services.UsersServices
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IRepository<Guid, Title> _titleRepository;
        private readonly IUserRepository _userRepository;

        public ExperienceService(IRepository<Guid, Title> titleRepository, IExperienceRepository experienceRepository, IUserRepository userRepository)
        {
            _experienceRepository = experienceRepository;
            _titleRepository = titleRepository;
            _userRepository = userRepository;
        }

        public async Task<Experience> AddExperience(AddExperienceDto experienceDto, Guid UserId)
        {
            _ = await _titleRepository.Get(experienceDto.TitleId) ?? throw new TitleNotFoundException("Invalid TitleId. Title does not exist.");

            var user = await _userRepository.Get(UserId);
            ValidateExperienceDates(user.Dob, DateOnly.FromDateTime(experienceDto.StartYear), DateOnly.FromDateTime(experienceDto.EndYear));

            var experience = new Experience
            {
                UserId = UserId,
                CompanyName = experienceDto.CompanyName,
                TitleId = experienceDto.TitleId,
                StartYear = DateOnly.FromDateTime(experienceDto.StartYear),
                EndYear = DateOnly.FromDateTime(experienceDto.EndYear)
            };

            return await _experienceRepository.Add(experience);
           // return ToDto(addedExperience);
        }

        public async Task<Experience> UpdateExperience(GetExperienceDto experienceDto, Guid UserId)
        {
            _ = await _titleRepository.Get(experienceDto.TitleId) ?? throw new TitleNotFoundException("Invalid TitleId. Title does not exist.");

            var experience = await _experienceRepository.Get(experienceDto.ExperienceId, UserId) ?? throw new ExperienceNotFoundException("Experience not found");

            var user = await _userRepository.Get(UserId);
            ValidateExperienceDates(user.Dob, DateOnly.FromDateTime(experienceDto.StartYear), DateOnly.FromDateTime(experienceDto.EndYear));

            experience.CompanyName = experienceDto.CompanyName;
            experience.TitleId = experienceDto.TitleId;
            experience.StartYear = DateOnly.FromDateTime(experienceDto.StartYear);
            experience.EndYear = DateOnly.FromDateTime(experienceDto.EndYear);

            return await _experienceRepository.Update(experience);
        }

        public async Task<bool> DeleteExperience(Guid experienceId, Guid UserId)
        {
            return await _experienceRepository.Delete(await _experienceRepository.Get(experienceId, UserId) ?? throw new ExperienceNotFoundException("Experience not found"));
        }

        public async Task<Experience> GetExperience(Guid experienceId, Guid userId)
        {
            var experience = await _experienceRepository.Get(experienceId, userId);
            return experience ?? throw new ExperienceNotFoundException("Experience not found");
        }

        public async Task<IEnumerable<Experience>> GetAllExperiences(Guid UserId)
        {
            var experiences = await _experienceRepository.GetAll(UserId);
            if (!experiences.Any())
            {
                throw new ExperienceNotFoundException("Experience not found");
            }
            return experiences;
        }

        private void ValidateExperienceDates(DateOnly birthDate, DateOnly startYear, DateOnly endYear)
        {
            if (startYear < birthDate || endYear < birthDate)
            {
                throw new InvalidExperienceDateException("Experience start or end date cannot be earlier than user's date of birth");
            }
        }

        public static ExperienceDto ToDto(Experience experience)
        {
            return new ExperienceDto
            {
                ExperienceId = experience.ExperienceId,
                CompanyName = experience.CompanyName,
                TitleName = experience.Title.TitleName,
                StartYear = experience.StartYear,
                EndYear = experience.EndYear,
                ExperienceDuration = (experience.EndYear.Year - experience.StartYear.Year)
            };
        }
    }
}
