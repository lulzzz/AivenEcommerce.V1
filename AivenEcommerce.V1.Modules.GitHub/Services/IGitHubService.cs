using AivenEcommerce.V1.Modules.GitHub.Dto;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Modules.GitHub.Services
{
    public interface IGitHubService
    {
        Task CreateFileAsync(long repositoryId, string path, string fileName, string content);
        Task<GhFileContent> GetFileContentAsync(long repositoryId, string path, string fileName);
        Task<bool> ExistFileAsync(long repositoryId, string path, string fileName);
        Task<IEnumerable<GhFileContent>> GetAllFilesAsync(long repositoryId, string path);
        Task<IEnumerable<GhFileContent>> GetAllDirectoriesAsync(long repositoryId, string path);
        Task<IEnumerable<GhFileContent>> GetAllFilesWithContentAsync(long repositoryId, string path);
        Task DeleteFileAsync(long repositoryId, string path, string fileName);
        Task UpdateFileAsync(long repositoryId, string path, string fileName, string content);
    }
}
