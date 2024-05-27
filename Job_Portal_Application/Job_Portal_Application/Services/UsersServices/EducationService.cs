using Job_Portal_Application.Dto.EducationDto;
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
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        private IAuthorizeService _authorizeService;


        public EducationService(IEducationRepository educationRepository, IAuthorizeService authorizeService)
        {
            _educationRepository = educationRepository;
            _authorizeService = authorizeService;


        }

        public async Task<Education> AddEducation(AddEducationDto educationDto)
        {



            var education = new Education
            {
                UserId = _authorizeService.Gettoken(),

                Level = educationDto.Level,
                InstitutionName = educationDto.InstitutionName,
                StartYear = DateOnly.FromDateTime(educationDto.StartYear),
                EndYear = educationDto.EndYear.HasValue ? DateOnly.FromDateTime(educationDto.EndYear.Value) : null,
                Percentage = educationDto.Percentage,
                IsCurrentlyStudying = educationDto.IsCurrentlyStudying
            };

            var addedEducation = await _educationRepository.Add(education);
            return addedEducation;
        }

        public async Task<Education> UpdateEducation(EducationDto educationDto)
        {
            var education = await _educationRepository.Get(educationDto.EducationId) ?? throw new EducationNotFoundException("Education record not found");
            education.EducationId = educationDto.EducationId;
            education.Level = educationDto.Level;
            education.InstitutionName = educationDto.InstitutionName;
            education.StartYear = DateOnly.FromDateTime(educationDto.StartYear);
            education.EndYear = educationDto.EndYear.HasValue ? DateOnly.FromDateTime(educationDto.EndYear.Value) : null;
            education.Percentage = educationDto.Percentage;
            education.IsCurrentlyStudying = educationDto.IsCurrentlyStudying;
      
  
            return await _educationRepository.Update(education);

        }

        public async Task<bool> DeleteEducation(Guid educationId)
        {
       
            return await _educationRepository.Delete(await _educationRepository.Get(educationId) ?? throw new EducationNotFoundException("Education record not found"));
        }

        public async Task<EducationDto> GetEducation(Guid educationId)
        {
            var education = await _educationRepository.Get(educationId) ?? throw new EducationNotFoundException("Education record not found");


            return new EducationDto
            {
                EducationId = education.EducationId,
                Level = education.Level,
                InstitutionName = education.InstitutionName,
                StartYear = education.StartYear.ToDateTime(TimeOnly.MinValue),
                EndYear = education.EndYear?.ToDateTime(TimeOnly.MinValue),
                Percentage = education.Percentage,
                IsCurrentlyStudying = education.IsCurrentlyStudying
            };
        }

        public async Task<IEnumerable<EducationDto>> GetAllEducations()
        {


            var educations = await _educationRepository.GetAll(_authorizeService.Gettoken());

            if (!educations.Any())
            {
                throw new EducationNotFoundException("Education record not found");
            }

            var educationDtos = educations.Select(education => new EducationDto
            {
                EducationId = education.EducationId,
                Level = education.Level,
                InstitutionName = education.InstitutionName,
                StartYear = education.StartYear.ToDateTime(TimeOnly.MinValue),
                EndYear = education.EndYear?.ToDateTime(TimeOnly.MinValue),
                Percentage = education.Percentage,
                IsCurrentlyStudying = education.IsCurrentlyStudying
            }).ToList();

            return educationDtos;
        }



    }
}
