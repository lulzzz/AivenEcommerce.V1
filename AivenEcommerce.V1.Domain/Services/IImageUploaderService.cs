using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IImageUploaderService
    {
        Task<Uri> UploadImage(byte[] image);
    }
}
