using Job_Portal_Application.Models;

namespace Job_Portal_Application.Services.UsersServices
{
    public interface IAdminService
    {
        Task<Skill> CreateSkill(Skill skill);
        Task<bool> DeleteSkill(Guid skillId);

        Task<Title> CreateTitle(Title title);
        Task<bool> DeleteTitle(Guid titleId);

    }

}
