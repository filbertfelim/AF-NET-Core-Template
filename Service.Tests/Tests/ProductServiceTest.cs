
using TestProject.DAL.DTO;
using TestProject.DAL.Models;
using TestProject.DAL.Interfaces;
using TestProject.Service.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace TestProject.Service.Tests.Tests
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _mockRepository = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task Create_ReturnsDto_WhenEntityCreated()
        {
            // Arrange
            var dto = new ProductDto { Id = "Test Id", Name = "Test Name" };;
            var entity = new Product { Id = "Test Id", Name = "Test Name" };;
            _mockRepository.Setup(r => r.Create(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.Create(dto);

            // Assert
            _mockRepository.Verify(r => r.Create(It.Is<Product>(e => e.Id == dto.Id && e.Name == dto.Name)), Times.Once);
            Assert.Equal(dto.Id, result.Id);
            Assert.Equal(dto.Name, result.Name);
        }

        [Fact]
        public async Task Read_ReturnsDto_WhenEntityFound()
        {
            // Arrange
            var entity = new Product { Id = "Test Id", Name = "Test Name" };;
            _mockRepository.Setup(r => r.Read(It.IsAny<string>())).ReturnsAsync(entity);

            // Act
            var result = await _service.Read("Test Id");

            // Assert
            _mockRepository.Verify(r => r.Read(It.Is<string>(id => id == "Test Id")), Times.Once);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task Read_ReturnsNull_WhenEntityNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.Read(It.IsAny<string>())).ReturnsAsync((Product)null);

            // Act
            var result = await _service.Read("1");

            // Assert
            _mockRepository.Verify(r => r.Read(It.Is<string>(id => id == "1")), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ReturnsDto_WhenEntityUpdated()
        {
            // Arrange
            var dto = new ProductDto { Id = "Test Id", Name = "Test Name" };;
            var entity = new Product { Id = "Test Id", Name = "Test Name" };;
            _mockRepository.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(entity);

            // Act
            var result = await _service.Update("Test Id", dto);

            // Assert
            _mockRepository.Verify(r => r.Update(It.Is<Product>(e => e.Id == dto.Id && e.Name == dto.Name)), Times.Once);
            Assert.Equal(dto.Id, result.Id);
            Assert.Equal(dto.Name, result.Name);
        }

        [Fact]
        public async Task Update_ReturnsNull_WhenEntityNotUpdated()
        {
            // Arrange
            var dto = new ProductDto { Id = "Test Id", Name = "Test Name" };;
            _mockRepository.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync((Product)null);

            // Act
            var result = await _service.Update("Test Id", dto);

            // Assert
            _mockRepository.Verify(r => r.Update(It.Is<Product>(e => e.Id == dto.Id && e.Name == dto.Name)), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ReturnsTrue_WhenEntityDeleted()
        {
            // Arrange
            _mockRepository.Setup(r => r.Delete(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _service.Delete("1");

            // Assert
            _mockRepository.Verify(r => r.Delete(It.Is<string>(id => id == "1")), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ReturnsFalse_WhenEntityNotDeleted()
        {
            // Arrange
            _mockRepository.Setup(r => r.Delete(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _service.Delete("1");

            // Assert
            _mockRepository.Verify(r => r.Delete(It.Is<string>(id => id == "1")), Times.Once);
            Assert.False(result);
        }
    }
}
