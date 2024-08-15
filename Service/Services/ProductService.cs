
using TestProject.DAL.DTO;
using TestProject.Service.Interfaces;
using TestProject.DAL.Models;
using TestProject.DAL.Interfaces;
using System.Threading.Tasks;

namespace TestProject.Service.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task<ProductDto> Create(ProductDto dto)
        {
            var entity = new Product
            {
                Id = dto.Id,
                Name = dto.Name
                // Map other properties
            };
            await _productRepository.Create(entity);
            return dto;
        }

        public async Task<ProductDto> Read(string id)
        {
            var entity = await _productRepository.Read(id);
            return entity == null ? null : new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name
                // Map other properties
            };
        }

        public async Task<ProductDto> Update(string id, ProductDto dto)
        {
            var entity = new Product
            {
                Id = id,
                Name = dto.Name
                // Map other properties
            };
            var updatedEntity = await _productRepository.Update(entity);
            return updatedEntity == null ? null : new ProductDto
            {
                Id = updatedEntity.Id,
                Name = updatedEntity.Name
                // Map other properties
            };
        }

        public async Task<bool> Delete(string id)
        {
            return await _productRepository.Delete(id);
        }
    }
}