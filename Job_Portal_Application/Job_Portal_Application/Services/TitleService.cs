using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Repository.SkillRepos;

namespace Job_Portal_Application.Services
{
    public class TitleService : ITitleService
    {
        private readonly IRepository<Guid, Title> _titleRepository;

        public TitleService(IRepository<Guid, Title> titleRepository)
        {
            _titleRepository = titleRepository;
        }

        public async Task<Title> GetTitle(Guid id)
        {
            return await _titleRepository.Get(id) ?? throw new TitleNotFoundException("Title not found");
        }

        public async Task<IEnumerable<Title>> GetAllTitles()
        {
            var titles= await _titleRepository.GetAll();
            if(!titles.Any()) throw new TitleNotFoundException("Title not found");
            return titles;  
        }

        public async Task<Title> AddSkills(Title title)
        {
            return await _titleRepository.Add(title);
        }

        public async Task<bool> DeleteSkills(Title title)
        {
            return await _titleRepository.Delete(title);
        }
    }
}
