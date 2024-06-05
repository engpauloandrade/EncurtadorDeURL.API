using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LinkCutter.Domain.Model;

public class UrlModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string OriginalUrl { get; set; }
    public string RashCode { get; set; }
    public DateTime CreatedAt { get; set; }

}
