using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public  interface ICompanyRepository : IRepository<Guid, Company>
    {
        Task<Company> GetByEmail(string email);
    }
}