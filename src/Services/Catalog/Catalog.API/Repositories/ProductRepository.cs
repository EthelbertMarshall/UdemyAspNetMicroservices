using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(p=>true).ToListAsync();

        }
        public async Task<Product> GetProductById(string id)
        {
            return await _catalogContext.Products.Find(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(z => z.Category, categoryName);

            return await _catalogContext.Products.Find(filterDefinition).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(x=>x.Name, name);

            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result=  await _catalogContext.Products.ReplaceOneAsync(filter:p=>p.Id==product.Id,replacement:product);

            return result.IsAcknowledged && result.ModifiedCount> 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            var result = await _catalogContext.Products.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

    }
}
