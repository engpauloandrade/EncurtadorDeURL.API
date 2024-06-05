namespace LinkCutter.Application.Interfaces;

public interface ISecurityService
{
    Task<bool> ValidateTokenAsync(string token);
}
