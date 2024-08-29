using ISSHAR.API.Interfaces;

namespace ISSHAR.API.Middlewares
{
    public class JwtWhitelistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtWhitelistService _jwtWhitelistService;
        public JwtWhitelistMiddleware(RequestDelegate next, IJwtWhitelistService jwtWhitelistService)
        {
            _next = next;
            _jwtWhitelistService = jwtWhitelistService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token) && !await _jwtWhitelistService.IsTokenWhitelistedAsync(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Token is not whitelisted.");
                return;
            }
            await _next(context);
        }
    }
}
