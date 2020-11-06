using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.Products;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductValidator
    {
        ValidationResult ValidateCreateProduct(CreateProductInput input);
        Task<ValidationResult> ValidateUpdateProduct(UpdateProductInput input);
        Task<ValidationResult> ValidateDeleteProduct(DeleteProductInput input);
        Task<ValidationResult> ValidateGetProduct(GetProductInput input);
    }
}
