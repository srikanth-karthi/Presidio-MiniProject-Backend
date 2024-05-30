using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface ITitleService
    {
        Task<Title> GetTitle(Guid id);
        Task<IEnumerable<Title>> GetAllTitles();
        Task<bool> DeleteSkills(Title title);
        Task<Title> AddSkills(Title title);
    }
}
