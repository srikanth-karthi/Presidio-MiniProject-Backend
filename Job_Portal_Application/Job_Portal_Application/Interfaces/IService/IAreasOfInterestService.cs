using Job_Portal_Application.Dto.AreasOfInterestDtos;

using Job_Portal_Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IAreasOfInterestService
    {
        Task<AreasOfInterest> AddAreasOfInterest(AddAreasOfInterestDTO areasOfInterestDto, Guid UserId);
        Task<AreasOfInterest> UpdateAreasOfInterest(AreasOfInterestDto areasOfInterestDto, Guid UserId);
        Task<bool> DeleteAreasOfInterest(Guid areasOfInterestId, Guid UserId);
        Task<AreasOfInterest> GetAreasOfInterest(Guid areasOfInterestId, Guid UserId);
        Task<IEnumerable<AreasOfInterest>> GetAllAreasOfInterest( Guid UserId);
    }
}
