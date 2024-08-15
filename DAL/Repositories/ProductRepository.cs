
using TestProject.DAL.Interfaces;
using TestProject.DAL.Models;

namespace TestProject.DAL.Interfaces
{
    public class ProductRepository : IProductRepository
    {
        public async Task Create(Product entity)
        {
            // Implementation for creating an entity
        }

        public async Task<Product> Read(string id)
        {
            // Implementation for reading an entity
            return null;
        }

        public async Task<Product> Update(Product entity)
        {
            // Implementation for updating an entity
            return null;
        }

        public async Task<bool> Delete(string id)
        {
            // Implementation for deleting an entity
            return true;
        }
    }
}