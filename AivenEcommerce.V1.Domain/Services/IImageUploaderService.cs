using AivenEcommerce.V1.Domain.Common;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IImageUploaderService : IScopedService
    {
        Task<Uri> UploadImage(byte[] image);
    }
}
