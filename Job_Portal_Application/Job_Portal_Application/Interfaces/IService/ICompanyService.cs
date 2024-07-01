using Job_Portal_Application.Dto.CompanyDto;
using Job_Portal_Application.Dto.CompanyDtos;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface ICompanyService
    {
        Task<CompanyDto> Register(CompanyRegisterDto companyDto);
        Task<string> Login(LoginDto companyDto);
        Task<CompanyDto> UpdateCompany(CompanyUpdateDto companyDto, Guid guid);
        Task<bool> DeleteCompany(Guid guid);
        Task<CompanyDto> GetCompany(Guid companyId);
        Task<IEnumerable<CompanyDto>> GetAllCompanies();
        Task<string> UploadCompanyLogo(Guid companyId, IFormFile logo);
   Task<bool> DeleteCompanyLogo(Guid companyId);
    }
}
