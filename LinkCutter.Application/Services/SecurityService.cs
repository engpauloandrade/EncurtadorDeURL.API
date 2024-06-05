using LinkCutter.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using NLog;

namespace LinkCutter.Application.Services;

public class SecurityService : ISecurityService
{
    private readonly IConfiguration _configuration;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private const string TOKEN_SECURITY_PATH = "SecurityToken:ApiToken";
    public SecurityService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<bool> ValidateTokenAsync(string token)
    {
        try
        {
            var expectedToken = _configuration.GetSection(TOKEN_SECURITY_PATH).Value;
            if (token == expectedToken)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao validar token");
            return await Task.FromResult(false);
        }

    }

}
