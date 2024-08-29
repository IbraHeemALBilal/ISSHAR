namespace ISSHAR.API.Interfaces
{
    public interface IJwtWhitelistService
    {
        Task AddTokenAsync(string token);
        Task<bool> IsTokenWhitelistedAsync(string token);
        Task RemoveTokenAsync(string token);
    }
}
