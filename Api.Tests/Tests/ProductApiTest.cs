
using TestProject.Api.Functions;
using TestProject.Service.Interfaces;
using TestProject.DAL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;
using System.IO;
using System.Threading.Tasks;

namespace TestProject.Api.Tests.Tests
{
    public class ProductApiTest
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductApi _function;

        public ProductApiTest()
        {
            _mockService = new Mock<IProductService>();
            _function = new ProductApi(_mockService.Object);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WhenValidRequest()
        {
            // Arrange
            var httpRequest = CreateHttpRequest("{ \"Id\": \"Test Id\", \"Name\": \"Test Name\"}");
            var dto = new ProductDto { Id = "Test Id", Name = "Test Name" };
            _mockService.Setup(service => service.Create(It.IsAny<ProductDto>())).ReturnsAsync(dto);

            // Act
            var result = await _function.Create(httpRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(dto.Id, returnValue.Id);
        }

        [Fact]
        public async Task Read_ReturnsOkResult_WhenEntityFound()
        {
            // Arrange
            var httpRequest = CreateHttpRequest();
            var dto = new ProductDto { Id = "Test Id", Name = "Test Name" };
            _mockService.Setup(service => service.Read(It.IsAny<string>())).ReturnsAsync(dto);

            // Act
            var result = await _function.Read(httpRequest, "Test Id");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(dto.Id, returnValue.Id);
        }

        [Fact]
        public async Task Read_ReturnsNotFoundResult_WhenEntityNotFound()
        {
            // Arrange
            var httpRequest = CreateHttpRequest();
            _mockService.Setup(service => service.Read(It.IsAny<string>())).ReturnsAsync((ProductDto)null);

            // Act
            var result = await _function.Read(httpRequest, "non-existent-id");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenEntityFound()
        {
            // Arrange
            var httpRequest = CreateHttpRequest("{ \"Id\": \"Test Id\", \"Name\": \"Test Name\"}");
            var dto = new ProductDto { Id = "Test Id", Name = "Test Name" };
            _mockService.Setup(service => service.Update(It.IsAny<string>(), It.IsAny<ProductDto>())).ReturnsAsync(dto);

            // Act
            var result = await _function.Update(httpRequest, "Test Id");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(dto.Id, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsNotFoundResult_WhenEntityNotFound()
        {
            // Arrange
            var httpRequest = CreateHttpRequest("{ \"Id\": \"Test Id\", \"Name\": \"Test Name\"}");
            _mockService.Setup(service => service.Update(It.IsAny<string>(), It.IsAny<ProductDto>())).ReturnsAsync((ProductDto)null);

            // Act
            var result = await _function.Update(httpRequest, "non-existent-id");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenEntityDeleted()
        {
            // Arrange
            var httpRequest = CreateHttpRequest();
            _mockService.Setup(service => service.Delete(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _function.Delete(httpRequest, "1");

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenEntityNotFound()
        {
            // Arrange
            var httpRequest = CreateHttpRequest();
            _mockService.Setup(service => service.Delete(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _function.Delete(httpRequest, "non-existent-id");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private HttpRequest CreateHttpRequest(string body = "")
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            if (!string.IsNullOrEmpty(body))
            {
                var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(body));
                request.Body = stream;
                stream.Seek(0, SeekOrigin.Begin);
            }
            return request;
        }
    }
}
