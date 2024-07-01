using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.CompanyDto;
using Job_Portal_Application.Dto.CompanyDtos;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository;

namespace Job_Portal_Application.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRepository<Guid, Credential> _credentialRepository;
        private readonly MinIOService _minioService;

        public CompanyService(IRepository<Guid, Credential> credentialRepository, ICompanyRepository companyRepository, IUserRepository userRepository, ITokenService tokenService, MinIOService minioService)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _credentialRepository = credentialRepository;
            _minioService = minioService;
        }
        public async Task<CompanyDto> Register(CompanyRegisterDto companyDto)
        {
            var existingUser = await _userRepository.GetByEmail(companyDto.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User is already registered as a user and cannot register as a Company.");
            }


            if (await _companyRepository.GetByEmail(companyDto.Email) != null)
            {
                throw new UserAlreadyExistsException("User is already registered");
            }
            HMACSHA512 hmacSha = new HMACSHA512();


            var creadentials = await _credentialRepository.Add(new Credential()
            {

                Password = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(companyDto.Password)),
                HasCode = hmacSha.Key,
                Role = Roles.Company,

            });

   

            var newCompany = new Company
            {
                CompanyName = companyDto.CompanyName,
                Email = companyDto.Email,
                CompanyAddress = companyDto.CompanyAddress,
                City = companyDto.City,
                CredentialId=creadentials.CredentialId,
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


            var credential = await _credentialRepository.Get(company.CredentialId);
            using (HMACSHA512 hmacSha = new HMACSHA512(credential.HasCode))
            {
                var encryptedPass = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(companyDto.Password));
                if (_tokenService.VerifyPassword(credential.Password, encryptedPass))
                {
                    var token = _tokenService.GenerateToken(company.CompanyId, credential.Role);
                    return token;
                }
            }

            throw new InvalidCredentialsException("Invalid password.");
        }

        public async Task<CompanyDto> UpdateCompany(CompanyUpdateDto companyDto, Guid UserId)
        {
            var company = await _companyRepository.Get(UserId) ?? throw new CompanyNotFoundException("Company not found.");

            company.CompanyName = companyDto.CompanyName;
            company.CompanyAddress = companyDto.CompanyAddress;
            company.City = companyDto.City;
            company.CompanySize = companyDto.CompanySize;
            company.CompanyWebsite = companyDto.CompanyWebsite;
            company.CompanyDescription = companyDto.CompanyDescription;

            var updatedCompany = await _companyRepository.Update(company);
            return MapToCompanyDto(updatedCompany);
        }

        public async Task<bool> DeleteCompany(Guid UserId)
        {
            var company = await _companyRepository.Get(UserId) ?? throw new CompanyNotFoundException("Company not found.");

            return await _companyRepository.Delete(company);
        }

        public async Task<CompanyDto> GetCompany(Guid companyId)
        {
            var company = await _companyRepository.Get(companyId) ?? throw new CompanyNotFoundException("Company not found.");
            return MapToCompanyDto(company);
        }
        public async Task<String> UploadCompanyLogo(Guid companyId, IFormFile logo)
        {
            var company = await _companyRepository.Get(companyId) ?? throw new CompanyNotFoundException("Company not found.");

            if (logo == null || logo.Length == 0)
                throw new ArgumentException("No logo selected");

            var extension = Path.GetExtension(logo.FileName).ToLowerInvariant();
            if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".svg")
                throw new ArgumentException("Invalid file type. Only JPG, JPEG, and PNG files are allowed.");

            var uniqueFileName = $"company-logo/{companyId}{extension}";

            using (var stream = logo.OpenReadStream())
            {
                await _minioService.UploadFileAsync(uniqueFileName, stream);
            }

            var logoUrl = $"{_minioService.GetServiceUrl()}{_minioService.GetBucketName()}/{uniqueFileName}";
            company.LogoUrl = logoUrl;

            await _companyRepository.Update(company);

            return logoUrl;
        }
        public async Task<bool> DeleteCompanyLogo(Guid companyId)
        {
            var company = await _companyRepository.Get(companyId) ?? throw new CompanyNotFoundException("Company not found.");

            if (string.IsNullOrEmpty(company.LogoUrl))
                throw new InvalidOperationException("Company does not have a logo to delete.");

            var logoPath = company.LogoUrl.Replace($"{_minioService.GetServiceUrl()}{_minioService.GetBucketName()}/", "");
            await _minioService.DeleteFileAsync(logoPath);

            company.LogoUrl = null;
            await _companyRepository.Update(company);

            return true;
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
                logoUrl= company.LogoUrl,
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                Email = company.Email,
                CompanyAddress = company.CompanyAddress,
                City = company.City,
                CompanySize = company.CompanySize,
                CompanyWebsite = company.CompanyWebsite,
                CompanyDescription = company.CompanyDescription,
            };
        }
    }
}
