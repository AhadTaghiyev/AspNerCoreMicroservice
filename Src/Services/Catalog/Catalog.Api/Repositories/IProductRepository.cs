using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProductsAsync();
        public Task<Product> GetProductAsync(string id);
        public Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName);
        public Task CreateProduct(Product product);
        public Task<bool> UpdateProduct(Product product);
        public Task<bool> DeleteProduct(string id);

    }
}
