using LinkCutter.Application.Interfaces;
using LinkCutter.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCutter.Application.InjecaoDependencia;

public static class InjecaoDependencia
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUrlService, UrlService>();

        return services;
    }

    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddScoped<ISecurityService, SecurityService>();
        return services;
    }
}
