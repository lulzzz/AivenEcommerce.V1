﻿using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.ProductCategories
{
    public record UpdateProductCategoryInput(Guid Id, string Name, IEnumerable<string> SubCategories);

}
