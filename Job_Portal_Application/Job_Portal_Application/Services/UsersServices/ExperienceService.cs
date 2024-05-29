using Job_Portal_Application.Dto.EducationDtos;
using Job_Portal_Application.Dto.ExperienceDtos;
using Job_Portal_Application.Dto.profile;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Job_Portal_Application.Services.UsersServices
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;

      
        private readonly IRepository<Guid, Title> _titleRepository;

        public ExperienceService(IRepository<Guid, Title> titleRepository,IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;


            _titleRepository = titleRepository;
        }

        public async Task<ExperienceDto> AddExperience(AddExperienceDto experienceDto, Guid UserId)
        {
            _ = await _titleRepository.Get(experienceDto.TitleId) ?? throw new TitleNotFoundException("Invalid TitleId. Title does not exist.");
            
            
            var experience = new Experience
            {
                UserId =UserId,
                CompanyName = experienceDto.CompanyName,
                TitleId = experienceDto.TitleId,
                StartYear = DateOnly.FromDateTime(experienceDto.StartYear),
                EndYear = DateOnly.FromDateTime(experienceDto.EndYear)
            };

            var addedExperience = await _experienceRepository.Add(experience);
            return ToDto(addedExperience);
        }

        public async Task<ExperienceDto> UpdateExperience(GetExperienceDto experienceDto, Guid UserId)
        {
            _ = await _titleRepository.Get(experienceDto.TitleId) ?? throw new TitleNotFoundException("Invalid TitleId. Title does not exist.");

         var experience=   await _experienceRepository.Get(experienceDto.ExperienceId, UserId) ?? throw new ExperienceNotFoundException("Experience not found ");


            experience.CompanyName = experienceDto.CompanyName;
            experience.TitleId = experienceDto.TitleId;
            experience.StartYear = DateOnly.FromDateTime(experienceDto.StartYear);
            experience.EndYear = DateOnly.FromDateTime(experienceDto.EndYear);

            return ToDto(await _experienceRepository.Update(experience));
        }

        public async Task<bool> DeleteExperience(Guid experienceId, Guid UserId)
        {
            return await _experienceRepository.Delete(await _experienceRepository.Get(experienceId, UserId) ?? throw new ExperienceNotFoundException("Experience not found "));
        }

        public async Task<ExperienceDto> GetExperience(Guid experienceId, Guid userId)
        {
            var experience = await _experienceRepository.Get(experienceId, userId);
            return ToDto(experience) ?? throw new ExperienceNotFoundException("Experience not found");
        }



        public async Task<IEnumerable<ExperienceDto>> GetAllExperiences( Guid UserId)
        {
            var experiences=  await _experienceRepository.GetAll(UserId);
            if (!experiences.Any())
            {
                 throw new ExperienceNotFoundException("Experience not found ");
            }
            return experiences.Select(j=> ToDto(j));
        }

        public static ExperienceDto ToDto( Experience experience)
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
