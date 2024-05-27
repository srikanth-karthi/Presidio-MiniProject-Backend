using Job_Portal_Application.Dto.AreasOfInterestDto;

using Job_Portal_Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IAreasOfInterestService
    {
        Task<AreasOfInterest> AddAreasOfInterest(AddAreasOfInterestDTO areasOfInterestDto);
        Task<AreasOfInterest> UpdateAreasOfInterest(AreasOfInterestDto areasOfInterestDto);
        Task<bool> DeleteAreasOfInterest(Guid areasOfInterestId);
        Task<AreasOfInterest> GetAreasOfInterest(Guid areasOfInterestId);
        Task<IEnumerable<AreasOfInterest>> GetAllAreasOfInterest();
    }
}
