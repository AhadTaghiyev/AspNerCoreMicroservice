using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateProduct(Product product)
        {
           await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x=>x.Id,id);

            DeleteResult result=await _context
                                         .Products
                                           .DeleteOneAsync(filter);

            return result.IsAcknowledged &&
                result.DeletedCount > 0;
    
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _context
                           .Products
                             .Find(x=>x.Id== id)
                                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context
                            .Products
                               .Find(x=>true)
                                  .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);

            return await _context
                           .Products
                                .Find(filter)
                                       .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);

            return await _context
                           .Products
                                .Find(filter)
                                       .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            ReplaceOneResult UpdateResult = await _context
                              .Products
                                 .ReplaceOneAsync(filter: x =>x.Id== product.Id,replacement:product);

            return UpdateResult.IsAcknowledged&&
                UpdateResult.ModifiedCount>0;
        }
    }
}
