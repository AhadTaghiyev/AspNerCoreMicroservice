using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Collections;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            MongoClient client =new MongoClient(configuration.GetValue<string>("DatabasesSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabasesSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabasesSettings:CollectionName"));
        }
        public IMongoCollection<Product> Products { get; }
    }
}
