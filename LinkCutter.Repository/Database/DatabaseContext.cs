using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LinkCutter.Repository.Database;

public class DatabaseContext
{
    private readonly string _connectionString;
    private readonly IMongoDatabase _database;
    public DatabaseContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoConnection");
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase("urlsdb");
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
