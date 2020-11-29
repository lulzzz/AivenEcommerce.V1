using AivenEcommerce.V1.Modules.ImgBB.Dto;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Modules.ImgBB.Services
{
    public interface IImgBbService
    {
        Task<ImgBbResponse> UploadImage(byte[] image);
    }
}