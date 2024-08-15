
using TestProject.DAL.DTO;

namespace TestProject.Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> Create(ProductDto dto);
        Task<ProductDto> Read(string id);
        Task<ProductDto> Update(string id, ProductDto dto);
        Task<bool> Delete(string id);
    }
}