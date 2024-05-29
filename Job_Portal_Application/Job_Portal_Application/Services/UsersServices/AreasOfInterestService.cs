using Job_Portal_Application.Dto.AreasOfInterestDtos;
using Job_Portal_Application.Dto.EducationDtos;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application.Services.UsersServices
{
    public class AreasOfInterestService : IAreasOfInterestService
    {
        private readonly IAreasOfInterestRepository _areasOfInterestRepository;
  
        private readonly IRepository<Guid, Title> _titleRepository;

        public AreasOfInterestService(IRepository<Guid, Title> titleRepository,IAreasOfInterestRepository areasOfInterestRepository)
        {
            _areasOfInterestRepository = areasOfInterestRepository;

            _titleRepository = titleRepository;
        }

        public async Task<AreasOfInterest> AddAreasOfInterest(AddAreasOfInterestDTO areasOfInterestDto, Guid UserId)
        {
            _ = await _titleRepository.Get(areasOfInterestDto.TitleId) ?? throw new TitleNotFoundException("Invalid TitleId. Title does not exist.");
          
            var areasOfInterest = new AreasOfInterest
            {
                UserId = UserId,
                TitleId = areasOfInterestDto.TitleId,
                Lpa = areasOfInterestDto.Lpa,
            };

            var addedAreasOfInterest = await _areasOfInterestRepository.Add(areasOfInterest);
            return addedAreasOfInterest;
        }

        public async Task<AreasOfInterest> UpdateAreasOfInterest(AreasOfInterestDto areasOfInterestDto,Guid UserId)
        {
            var areasOfInterest = await _areasOfInterestRepository.Get(areasOfInterestDto.AreasOfInterestId, UserId)
                                  ?? throw new AreasOfInterestNotFoundException("Areas of Interest record not found");

            areasOfInterest.TitleId = areasOfInterestDto.TitleId;
            areasOfInterest.Lpa = areasOfInterestDto.Lpa;

            return await _areasOfInterestRepository.Update(areasOfInterest);
        }

        public async Task<bool> DeleteAreasOfInterest(Guid areasOfInterestId, Guid UserId)
        {
            var areasOfInterest = await _areasOfInterestRepository.Get(areasOfInterestId, UserId)
                                  ?? throw new AreasOfInterestNotFoundException("Areas of Interest record not found");
            return await _areasOfInterestRepository.Delete(areasOfInterest);
        }

        public async Task<AreasOfInterest> GetAreasOfInterest(Guid areasOfInterestId, Guid UserId)
        {
             return  await _areasOfInterestRepository.Get(areasOfInterestId, UserId)
                                  ?? throw new AreasOfInterestNotFoundException("Areas of Interest record not found");


        }

        public async Task<IEnumerable<AreasOfInterest>> GetAllAreasOfInterest( Guid UserId)
        {
            var areasOfInterests = await _areasOfInterestRepository.GetAll(UserId);

            if (!areasOfInterests.Any())
            {
                throw new AreasOfInterestNotFoundException("Areas of Interest records not found");
            }

   

            return areasOfInterests;
        }
    }
}
