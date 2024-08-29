using ISSHAR.API.Interfaces;

namespace ISSHAR.API.Services
{
    public class JwtWhitelistService : IJwtWhitelistService
    {
        private readonly HashSet<string> _whitelist = new HashSet<string>();
        public Task AddTokenAsync(string token)
        {
            _whitelist.Add(token);
            return Task.CompletedTask;
        }

        public Task<bool> IsTokenWhitelistedAsync(string token)
        {
            return Task.FromResult(_whitelist.Contains(token));
        }

        public Task RemoveTokenAsync(string token)
        {
            _whitelist.Remove(token);
            return Task.CompletedTask;
        }
    }
}
