using LinkCutter.Application.Interfaces;
using LinkCutter.Application.Response;
using LinkCutter.Domain.Model;
using LinkCutter.Repository.Database;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NLog;
using System.Security.Cryptography;
using System.Text;

namespace LinkCutter.Application.Services;

public class UrlService : IUrlService
{
    private readonly DatabaseContext _dbContext;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly string _collectionName;
    public UrlService(DatabaseContext dbContext, IConfiguration configuration)
    {
        _collectionName = configuration.GetSection("DatabaseSettings:CollectionName").Value;
        _dbContext = dbContext;
    }
    public async Task<UrlResponse> GetUrlAsync(string rash)
    {
        try
        {
            var urlsCollection = _dbContext.GetCollection<UrlModel>(_collectionName);
            var filter = Builders<UrlModel>.Filter.Eq(u => u.RashCode, rash);
            var urlModel = urlsCollection.Find(filter).FirstOrDefault();

            if (urlModel != null)
            {
                return new UrlResponse
                {
                    OriginalUrl = urlModel.OriginalUrl,
                    RashCode = urlModel.RashCode,
                    Message = "Url encontrada com sucesso!"
                };
            }
            else
            {
                return new UrlResponse
                {
                    Message = "Url não encontrada!"
                };
            }
        }
        catch(Exception ex)
        {
            _logger.Error(ex, "Erro ao buscar url");
            return new UrlResponse
            {
                Message = ex.Message
            };
        }
    }

    public async Task<UrlResponse> PostUrlAsync(string url)
    {
        var response = new UrlResponse();
        try
        {
            var rashCode = GenerateRashCode(url);

            var urlModel = new UrlModel
            {
                OriginalUrl = url,
                RashCode =  rashCode,
                CreatedAt = DateTime.Now
            };

            var urlsCollection = _dbContext.GetCollection<UrlModel>(_collectionName);

            urlsCollection.InsertOne(urlModel);

            return new UrlResponse
            {
                OriginalUrl = urlModel.OriginalUrl,
                RashCode = urlModel.RashCode,
                Message = "Url encurtada com sucesso!"
            };

        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao encurtar url");
            return new UrlResponse
            {
                Message = ex.Message
            };
        }
       
    }

    private static string GenerateRashCode(string url)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));
            return Convert.ToBase64String(bytes).Substring(0, 7);
        }
    }
}
