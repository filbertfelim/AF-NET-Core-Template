
using TestProject.DAL.Models;
using TestProject.DAL.Interfaces;
using Xunit;
using System.Threading.Tasks;

namespace TestProject.DAL.Tests
{
    public class ProductRepositoryTest
    {
        private readonly ProductRepository _repository;

        public ProductRepositoryTest()
        {
            _repository = new ProductRepository();
        }

        [Fact]
        public async Task Create_CanBeCalled()
        {
            // Arrange
            var entity = new Product { Id = "Test Id", Name = "Test Name" };

            // Act
            await _repository.Create(entity);

            // Assert
            // Since the method is not implemented, just ensuring it can be called
            Assert.True(true);
        }

        [Fact]
        public async Task Read_ReturnsNull_WhenNotImplemented()
        {
            // Arrange
            var id = "Test Id";

            // Act
            var result = await _repository.Read(id);

            // Assert
            Assert.Null(result); // Expected to be null since the method is not implemented
        }

        [Fact]
        public async Task Update_ReturnsNull_WhenNotImplemented()
        {
            // Arrange
            var entity = new Product { Id = "Test Id", Name = "Test Name" };

            // Act
            var result = await _repository.Update(entity);

            // Assert
            Assert.Null(result); // Expected to be null since the method is not implemented
        }

        [Fact]
        public async Task Delete_ReturnsTrue_WhenNotImplemented()
        {
            // Arrange
            var id = "Test Id";

            // Act
            var result = await _repository.Delete(id);

            // Assert
            Assert.True(result); // Expected to be true since the method returns true by default
        }
    }
}
