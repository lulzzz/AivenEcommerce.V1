using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Common;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IImageUploaderService : IScopedService
    {
        Task<Uri> UploadImage(byte[] image);
    }
}
