using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares;

public class JwtCookieMiddleware(RequestDelegate next)
{
    private const string CookieName = "token";
    private const string BearerPrefix = "Bearer ";

    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization")
            && context.Request.Cookies.TryGetValue(CookieName, out var token)
            && !string.IsNullOrWhiteSpace(token))
        {
            context.Request.Headers.Append("Authorization", BearerPrefix + token);
        }

        await _next(context);
    }
}
