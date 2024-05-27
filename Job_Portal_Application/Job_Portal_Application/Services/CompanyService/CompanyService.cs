using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.CompanyDto;
using Job_Portal_Application.Dto.CompanyDtos;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizeService _authorizeService;

        public CompanyService(IAuthorizeService authorizeService, ICompanyRepository companyRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _authorizeService = authorizeService;
        }

        public async Task<CompanyDto> Register(CompanyRegisterDto companyDto)
        {
            var existingUser = await _userRepository.GetByEmail(companyDto.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User is already registered as a user and cannot register as a Company.");
            }

            var existingCompany = await _companyRepository.GetByEmail(companyDto.Email);
            if (existingCompany != null)
            {
                throw new UserAlreadyExistsException($"{companyDto.Email} already used, please try with another email.");
            }

            HMACSHA512 hmacSha = new HMACSHA512();

            var newCompany = new Company
            {
                CompanyName = companyDto.CompanyName,
                Email = companyDto.Email,
                CompanyAddress = companyDto.CompanyAddress,
                City = companyDto.City,
                CompanySize = companyDto.CompanySize,
                CompanyWebsite = companyDto.CompanyWebsite,
                Password = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(companyDto.Password)),
                HasCode = hmacSha.Key
            };

            var addedCompany = await _companyRepository.Add(newCompany);
            return MapToCompanyDto(addedCompany);

        }

        public async Task<string> Login(LoginDto companyDto)
        {
            var company = await _companyRepository.GetByEmail(companyDto.Email);
            if (company == null)
            {
                throw new InvalidCredentialsException("Email not found.");
            }

            using (HMACSHA512 hmacSha = new HMACSHA512(company.HasCode))
            {
                var encryptedPass = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(companyDto.Password));
                if (_tokenService.VerifyPassword(company.Password, encryptedPass))
                {
                    var token = _tokenService.GenerateToken(company.CompanyId);
                    return token;
                }
            }

            throw new InvalidCredentialsException("Invalid password.");
        }

        public async Task<CompanyDto> UpdateCompany(CompanyUpdateDto companyDto)
        {
            var company = await _companyRepository.Get(_authorizeService.Gettoken()) ?? throw new CompanyNotFoundException("Company not found.");

            company.CompanyName = companyDto.CompanyName;
            company.CompanyAddress = companyDto.CompanyAddress;
            company.City = companyDto.City;
            company.CompanySize = companyDto.CompanySize;
            company.CompanyWebsite = companyDto.CompanyWebsite;

            var updatedCompany = await _companyRepository.Update(company);
            return MapToCompanyDto(updatedCompany);
        }

        public async Task<bool> DeleteCompany()
        {
            var company = await _companyRepository.Get(_authorizeService.Gettoken()) ?? throw new CompanyNotFoundException("Company not found.");

            return await _companyRepository.Delete(company);
        }

        public async Task<CompanyDto> GetCompany(Guid companyId)
        {
            var company = await _companyRepository.Get(companyId) ?? throw new CompanyNotFoundException("Company not found.");
            return MapToCompanyDto(company);
        }




        public async Task<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            var companies = await _companyRepository.GetAll();
            if (!companies.Any()) throw new CompanyNotFoundException("Company not found.");
            var companyDtos = new List<CompanyDto>();
            foreach (var company in companies)
            {
                companyDtos.Add(MapToCompanyDto(company));
            }
            return companyDtos;
        }





        private CompanyDto MapToCompanyDto(Company company)
        {
            return new CompanyDto
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                Email = company.Email,
                CompanyAddress = company.CompanyAddress,
                City = company.City,
                CompanySize = company.CompanySize,
                CompanyWebsite = company.CompanyWebsite
            };
        }
    }
}
