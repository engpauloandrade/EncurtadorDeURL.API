using LinkCutter.Domain.Middleware;
using Microsoft.AspNetCore.Builder;

namespace LinkCutter.Domain.DependencyInjection;

public static class DependencyInjection
{
    public static IApplicationBuilder AddErrorMiddleware(this IApplicationBuilder app, params object?[] args)
    {
        return app.UseMiddleware<ExceptionMiddleware>(args);
    }
}
