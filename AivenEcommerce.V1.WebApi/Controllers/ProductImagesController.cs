using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductImages;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Modules.ImgBB.Extensions;
using AivenEcommerce.V1.WebApi.Presenter;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.WebApi.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService ?? throw new ArgumentNullException(nameof(productImageService));
        }

        [HttpGet("product/{productId:length(24)}")]
        [ProducesResponseType(typeof(OperationResult<ProductImageDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAll(string productId)
        {
            var result = await _productImageService.GetAllImageAsync(productId);

            return new OperationActionResult(result);
        }


        [HttpPost("product/{productId:length(24)}/[action]")]
        [ProducesResponseType(typeof(OperationResult<ProductImageDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UploadFile(string productId, IFormFile file)
        {
            if (file is { Length: > 0 })
            {
                var result = await _productImageService.UploadImageAsync(productId, file.OpenReadStream().ReadFully());

                return new OperationActionResult(result);
            }

            return new OperationActionResult(OperationResult.Fail());
        }

        [HttpDelete("{id}/product/{productId:length(24)}")]
        [ProducesResponseType(typeof(OperationResult<ProductImageDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Delete(Guid id, string productId)
        {
            var result = await _productImageService.DeleteImageAsync(new DeleteProductImageInput(productId, id));

            return new OperationActionResult(result);
        }
    }
}
