using Job_Portal_Application.Dto.EducationDtos;
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
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IUserRepository _userRepository;

        public EducationService(IEducationRepository educationRepository, IUserRepository userRepository)
        {
            _educationRepository = educationRepository;
            _userRepository = userRepository;
        }

        public async Task<Education> AddEducation(AddEducationDto educationDto, Guid UserId)
        {
            var user = await _userRepository.Get(UserId) ?? throw new UserNotFoundException("User not found.");
            ValidateEducationDates(user.Dob, DateOnly.FromDateTime(educationDto.StartYear),

                educationDto.EndYear.HasValue ? DateOnly.FromDateTime(educationDto.EndYear.Value) : null);

            var education = new Education
            {
                UserId = UserId,
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

        public async Task<Education> UpdateEducation(EducationDto educationDto, Guid UserId)
        {
            var user = await _userRepository.Get(UserId) ?? throw new UserNotFoundException("User not found.");
            ValidateEducationDates(user.Dob, DateOnly.FromDateTime(educationDto.StartYear),
                educationDto.EndYear.HasValue ? DateOnly.FromDateTime(educationDto.EndYear.Value) : null);

            var education = await _educationRepository.Get(educationDto.EducationId, UserId) ??
                            throw new EducationNotFoundException("Education record not found");

            education.Level = educationDto.Level;
            education.InstitutionName = educationDto.InstitutionName;
            education.StartYear = DateOnly.FromDateTime(educationDto.StartYear);
            education.EndYear = educationDto.EndYear.HasValue ? DateOnly.FromDateTime(educationDto.EndYear.Value) : null;
            education.Percentage = educationDto.Percentage;
            education.IsCurrentlyStudying = educationDto.IsCurrentlyStudying;

            return await _educationRepository.Update(education);
        }

        public async Task<bool> DeleteEducation(Guid educationId, Guid UserId)
        {
            var education = await _educationRepository.Get(educationId, UserId) ??
                            throw new EducationNotFoundException("Education record not found");

            return await _educationRepository.Delete(education);
        }

        public async Task<EducationDto> GetEducation(Guid educationId, Guid UserId)
        {
            var education = await _educationRepository.Get(educationId, UserId) ??
                            throw new EducationNotFoundException("Education record not found");

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

        public async Task<IEnumerable<EducationDto>> GetAllEducations(Guid UserId)
        {
            var educations = await _educationRepository.GetAll(UserId);

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

        private void ValidateEducationDates(DateOnly birthDate, DateOnly startYear, DateOnly? endYear)
        {
            if (startYear < birthDate ||
                (endYear.HasValue && endYear.Value < birthDate))
            {
                throw new InvalidEducationDateException("Education start or end date cannot be earlier than user's date of birth");
            }
        }
    }
}
