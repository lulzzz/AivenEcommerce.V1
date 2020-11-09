using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Modules.ImgBB.Services;

namespace AivenEcommerce.V1.Application.Services
{
    public class ImageUploaderService : IImageUploaderService
    {
        private readonly IImgBbService _imgBbService;

        public ImageUploaderService(IImgBbService imgBbService)
        {
            _imgBbService = imgBbService ?? throw new ArgumentNullException(nameof(imgBbService));
        }

        public async Task<Uri> UploadImage(byte[] image)
        {
            var response = await _imgBbService.UploadImage(image);
            return response.Data.Image.Url;
        }
    }
}
