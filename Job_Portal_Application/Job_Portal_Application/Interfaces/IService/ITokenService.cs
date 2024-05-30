using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface ITokenService
    {


        public string GenerateToken(Guid id, Dto.Enums.Roles role);
        public bool VerifyPassword(byte[] encrypterPass, byte[] password);
    }
}
