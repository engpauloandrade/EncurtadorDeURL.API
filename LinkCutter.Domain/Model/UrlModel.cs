using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LinkCutter.Domain.Model;

public class UrlModel //mmd - modelagem de dados
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("originalUrl")]
    public string OriginalUrl { get; set; }

    [BsonElement("rashCode")]
    public string RashCode { get; set; }

    [BsonElement("createdAt")]
    public string CreatedAt { get; set; }

}
