﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.CompanyRepos;
using Job_Portal_Application.Repository.UserRepos;

namespace Job_Portal_Application.Services.UsersServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAuthorizeService _authorizeService;

        public UserService(IAuthorizeService  authorizeService, ICompanyRepository companyRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _companyRepository = companyRepository;
            _authorizeService = authorizeService;
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

        public async Task<IEnumerable<JobDto>> GetRecommendedJobs( int pageNumber, int pageSize)
        {
            return (await _userRepository.GetRecommendedJobsForUser(_authorizeService.Gettoken(), pageNumber, pageSize)).Select(j=> MapToJobDto(j));    
        }

        public async Task<UserDto> UpdateUser(UserDto userDto)
        {
            var user = await _userRepository.Get(_authorizeService.Gettoken()) ?? throw new UserNotFoundException("User not found.");
 

            user.Name = userDto.Name;
            user.Address = userDto.Address;
            user.City = userDto.City;
            user.PortfolioLink = userDto.PortfolioLink;
            user.Phonenumber = userDto.PhoneNumber;
            user.ResumeUrl = userDto.ResumeUrl;

            return ToUserDto(await _userRepository.Update(user));

        }

        public async Task<bool> DeleteUser()
        {
            var user = await _userRepository.Get(_authorizeService.Gettoken()) ?? throw new UserNotFoundException("User not found.");

            return await _userRepository.Delete(user);
        }


        public async Task<User> GetUserProfile(Guid userId)
        {
            var user = await _userRepository.GetUserProfile(userId) ?? throw new UserNotFoundException("User not found.");

            user.Password = [];
            user.HasCode = [];
            user.JobActivities = [];
            return user;



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