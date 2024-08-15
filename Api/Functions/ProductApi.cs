
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using TestProject.Service.Interfaces;
using TestProject.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace TestProject.Api.Functions
{
    public class ProductApi
    {
        private readonly IProductService _productService;

        public ProductApi(IProductService service)
        {
            _productService = service;
        }

        [Function("ProductCreate")]
        public async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ProductDto>(requestBody);
            var result = await _productService.Create(data);

            return new OkObjectResult(result);
        }

        [Function("ProductRead")]
        public async Task<IActionResult> Read(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id}")] HttpRequest req,
            string id)
        {
            var result = await _productService.Read(id);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        [Function("ProductUpdate")]
        public async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "product/{id}")] HttpRequest req,
            string id)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ProductDto>(requestBody);
            var result = await _productService.Update(id, data);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        [Function("ProductDelete")]
        public async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "product/{id}")] HttpRequest req,
            string id)
        {
            var result = await _productService.Delete(id);

            if (!result)
            {
                return new NotFoundResult();
            }

            return new OkResult();
        }
    }
}