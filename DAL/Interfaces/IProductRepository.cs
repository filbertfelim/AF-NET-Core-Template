
using TestProject.DAL.Models;

namespace TestProject.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task Create(Product entity);
        Task<Product> Read(string id);
        Task<Product> Update(Product entity);
        Task<bool> Delete(string id);
    }
}