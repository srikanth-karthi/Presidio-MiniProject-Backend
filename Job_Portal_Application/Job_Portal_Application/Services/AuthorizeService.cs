using Job_Portal_Application.Interfaces.IService;

namespace Job_Portal_Application.Services
{
    public class AuthorizeService: IAuthorizeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid Gettoken()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("id")?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User ID not found in the token.");
            }
            return Guid.Parse(userId);
        }
    }
}
