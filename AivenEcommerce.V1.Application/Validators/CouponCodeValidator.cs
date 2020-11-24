using System;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.CouponCodes;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Enums;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Validators
{
    public class CouponCodeValidator : ICouponCodeValidator
    {
        private readonly ICouponCodeRepository _couponCodeRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public CouponCodeValidator(ICouponCodeRepository couponCodeRepository, IProductCategoryRepository productCategoryRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _couponCodeRepository = couponCodeRepository ?? throw new ArgumentNullException(nameof(couponCodeRepository));
            _productCategoryRepository = productCategoryRepository ?? throw new ArgumentNullException(nameof(productCategoryRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ValidationResult> ValidateCreateCouponCodeAsync(CreateCouponCodeInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Code))
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Code), "Debe indicar un código."));
            }
            else
            {
                if (input.Code.Length < 3)
                {
                    validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Code), "El código debe contener más de 3 caracteres."));
                }
                else if (!input.Code.All(c => char.IsLetterOrDigit(c)))
                {
                    validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Code), "El código solo debe contener letras y numeros."));
                }
                else
                {
                    CouponCode couponCode = await _couponCodeRepository.GetCouponAsync(input.Code.ToUpper());

                    if (couponCode is not null)
                    {
                        validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Code), "Ya existe un código con ese nombre."));
                    }
                }
            }
            if (!Enum.IsDefined(input.Type))
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Type), "El tipo de código no existe."));
            }

            if (input.Value <= 0)
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Value), "El valor del código debe ser mayor a cero."));
            }

            if (input.Type == CouponCodeOffType.Percentage && input.Value > 100)
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Value), "Un código de tipo porcentaje no puede tener un valor mayor de 100."));
            }

            if (input.MinAmount < 0)
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.MinAmount), "El monto mínimo del código debe ser mayor o igual a cero."));
            }

            if (input.MaxAmount.HasValue && input.MaxAmount.Value <= 0)
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.MaxAmount), "El monto máximo del código debe ser mayor a cero."));
            }

            if (input.MaxAmount.HasValue && input.MaxAmount.Value < input.MinAmount)
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.MaxAmount), "El monto máximo del código no puede ser menor al monto mínimo."));
            }

            if (input.DateStart.ToUniversalTime().Date < DateTime.Today)
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.DateStart), "La fecha de inicio no puede ser anterior al día de hoy."));
            }

            if (input.DateExpire.HasValue && input.DateExpire.Value.ToUniversalTime().Date < DateTime.Today)
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.DateExpire), "La fecha de expiración no puede ser anterior al día de hoy."));
            }

            if (input.DateExpire.HasValue && input.DateExpire.Value.ToUniversalTime().Date < input.DateStart.ToUniversalTime().Date)
            {
                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.DateExpire), "La fecha de expiración no puedo ser anterior a la fecha de inicio."));
            }

            if (input.Products?.Any() ?? false)
            {
                var products = _productRepository.GetAvailableProducts(input.Products);
                if (input.Products.Distinct().WhereIsNotEmply().Count() > products.Count())
                {
                    validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Products), "Algunos productos no existen o no estan activos."));
                }
            }

            if (input.Customers?.Any() ?? false)
            {
                foreach (var customerEmail in input.Customers.Distinct().WhereIsNotEmply())
                {
                    var customer = await _customerRepository.GetCustomer(customerEmail);
                    if (customer is null)
                    {
                        validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Customers), $"El cliente {customerEmail} no existe."));
                    }
                }
            }

            if (input.Categories?.Any() ?? false)
            {
                foreach (var categoryName in input.Categories.Distinct().WhereIsNotEmply())
                {
                    var category = await _productCategoryRepository.GetByNameAsync(categoryName);
                    if (category is null)
                    {
                        validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.Categories), $"La categoria {categoryName} no existe."));
                    }
                }
            }

            if (input.SubCategories?.Any() ?? false)
            {
                var groups = input.SubCategories.GroupBy(x => x.Category);

                foreach (var group in groups)
                {
                    var category = await _productCategoryRepository.GetByNameAsync(group.Key);

                    if (category is null)
                    {
                        validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.SubCategories), $"La categoria {group.Key} no existe."));
                    }
                    else
                    {
                        foreach (var item in group)
                        {
                            if (!category.SubCategories.Contains(item.SubCategory))
                            {
                                validationResult.Messages.Add(new(nameof(CreateCouponCodeInput.SubCategories), $"La subcategoria {item.SubCategory} no existe en la categoria {category.Name}."));
                            }
                        }
                    }
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateRemoveCouponCodeAsync(RemoveCouponCodeInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Code))
            {
                validationResult.Messages.Add(new(nameof(RemoveCouponCodeInput.Code), "Debe indicar un código."));
            }
            else
            {
                CouponCode couponCode = await _couponCodeRepository.GetCouponAsync(input.Code.ToUpper());

                if (couponCode is null)
                {
                    validationResult.Messages.Add(new(nameof(RemoveCouponCodeInput.Code), $"El código {input.Code} no existe."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateCouponCodeAsync(UpdateCouponCodeInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Code))
            {
                validationResult.Messages.Add(new(nameof(RemoveCouponCodeInput.Code), "Debe indicar un código."));
            }
            else
            {
                CouponCode couponCode = await _couponCodeRepository.GetCouponAsync(input.Code.ToUpper());

                if (couponCode is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.Code), $"El código {input.Code} no existe."));
                }
            }

            if (!Enum.IsDefined(input.Type))
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.Type), "El tipo de código no existe."));
            }

            if (input.Value <= 0)
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.Value), "El valor del código debe ser mayor a cero."));
            }

            if (input.Type == CouponCodeOffType.Percentage && input.Value > 100)
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.Value), "Un código de tipo porcentaje no puede tener un valor mayor de 100."));
            }

            if (input.MinAmount < 0)
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.MinAmount), "El monto mínimo del código debe ser mayor o igual a cero."));
            }

            if (input.MaxAmount.HasValue && input.MaxAmount.Value <= 0)
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.MaxAmount), "El monto máximo del código debe ser mayor a cero."));
            }

            if (input.MaxAmount.HasValue && input.MaxAmount.Value < input.MinAmount)
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.MaxAmount), "El monto máximo del código no puede ser menor al monto mínimo."));
            }

            if (input.DateStart.ToUniversalTime().Date < DateTime.Today)
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.DateStart), "La fecha de inicio no puede ser anterior al día de hoy."));
            }

            if (input.DateExpire.HasValue && input.DateExpire.Value.ToUniversalTime().Date < DateTime.Today)
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.DateExpire), "La fecha de expiración no puede ser anterior al día de hoy."));
            }

            if (input.DateExpire.HasValue && input.DateExpire.Value.ToUniversalTime().Date < input.DateStart.ToUniversalTime().Date)
            {
                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.DateExpire), "La fecha de expiración no puedo ser anterior a la fecha de inicio."));
            }

            if (input.Products?.Any() ?? false)
            {
                var products = _productRepository.GetAvailableProducts(input.Products);
                if (input.Products.Distinct().WhereIsNotEmply().Count() > products.Count())
                {
                    validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.Products), "Algunos productos no existen o no estan activos."));
                }
            }

            if (input.Customers?.Any() ?? false)
            {
                foreach (var customerEmail in input.Customers.Distinct().WhereIsNotEmply())
                {
                    var customer = await _customerRepository.GetCustomer(customerEmail);
                    if (customer is null)
                    {
                        validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.Customers), $"El cliente {customerEmail} no existe."));
                    }
                }
            }

            if (input.Categories?.Any() ?? false)
            {
                foreach (var categoryName in input.Categories.Distinct().WhereIsNotEmply())
                {
                    var category = await _productCategoryRepository.GetByNameAsync(categoryName);
                    if (category is null)
                    {
                        validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.Categories), $"La categoria {categoryName} no existe."));
                    }
                }
            }

            if (input.SubCategories?.Any() ?? false)
            {
                var groups = input.SubCategories.GroupBy(x => x.Category);

                foreach (var group in groups)
                {
                    var category = await _productCategoryRepository.GetByNameAsync(group.Key);

                    if (category is null)
                    {
                        validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.SubCategories), $"La categoria {group.Key} no existe."));
                    }
                    else
                    {
                        foreach (var item in group)
                        {
                            if (!category.SubCategories.Contains(item.SubCategory))
                            {
                                validationResult.Messages.Add(new(nameof(UpdateCouponCodeInput.SubCategories), $"La subcategoria {item.SubCategory} no existe en la categoria {category.Name}."));
                            }
                        }
                    }
                }
            }

            return validationResult;
        }
    }
}
