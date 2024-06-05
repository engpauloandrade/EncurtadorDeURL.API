using LinkCutter.Application.Response;

namespace LinkCutter.Application.Interfaces;

public interface IUrlService
{
    public Task<UrlResponse> GetUrlAsync(string rash);
    public Task<UrlResponse> PostUrlAsync(string value);
}
