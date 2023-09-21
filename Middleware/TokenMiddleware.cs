using JamOrder.Models;
using JamOrder.Services.Interfaces;

namespace JamOrder.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public TokenMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!(context.Request.Path.StartsWithSegments("/api/user/register") || context.Request.Path.StartsWithSegments("/api/user/login")))
            {
                await CheckToken(context);
            }
            else
            {
                await _next(context);
            }
        }

        private async Task CheckToken(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (_tokenService.ValidateToken(token))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
