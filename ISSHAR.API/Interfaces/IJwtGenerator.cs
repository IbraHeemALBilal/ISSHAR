using ISSHAR.Application.DTOs.UserDTOs;

namespace ISSHAR.API.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateJwtToken(UserDisplayDTO user);
    }
}