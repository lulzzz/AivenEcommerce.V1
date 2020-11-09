using System.Threading.Tasks;

using AivenEcommerce.V1.Modules.ImgBB.Dto;

namespace AivenEcommerce.V1.Modules.ImgBB.Services
{
    public interface IImgBbService
    {
        Task<ImgBbResponse> UploadImage(byte[] image);
    }
}