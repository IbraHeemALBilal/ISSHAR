using ISSHAR.Application.DTOs.UserDTOs;

namespace ISSHAR.API
{
    public interface IJwtGenerator
    {
        string GenerateJwtToken(UserDisplayDTO user);
    }
}
