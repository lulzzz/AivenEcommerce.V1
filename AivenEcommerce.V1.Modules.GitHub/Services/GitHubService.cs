using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Modules.GitHub.Dto;

using Octokit;

namespace AivenEcommerce.V1.Modules.GitHub.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubClient _githubClient;

        public GitHubService(IGitHubClient githubClient)
        {
            _githubClient = githubClient;
        }

        public async Task CreateFileAsync(long repositoryId, string path, string fileName, string content)
        {
            await _githubClient.Repository.Content.CreateFile(
                    repositoryId,
                    Path.Combine(path, fileName),
                    new CreateFileRequest($"Create {fileName}",
                                          content,
                                          "main"));
        }

        public async Task DeleteFileAsync(long repositoryId, string path, string fileName)
        {
            IReadOnlyList<RepositoryContent> contents = await _githubClient.Repository.Content.GetAllContents(repositoryId, Path.Combine(path, fileName));
            RepositoryContent repositoryContent = contents.First();

            await _githubClient.Repository.Content.DeleteFile(
                repositoryId,
                    Path.Combine(path, fileName),
                    new DeleteFileRequest($"Delete {fileName}",
                                          repositoryContent.Sha,
                                          "main")
                    );
        }

        public async Task UpdateFileAsync(long repositoryId, string path, string fileName, string content)
        {
            IReadOnlyList<RepositoryContent> contents = await _githubClient.Repository.Content.GetAllContents(repositoryId, Path.Combine(path, fileName));
            RepositoryContent repositoryContent = contents.First();

            await _githubClient.Repository.Content.UpdateFile(
                repositoryId,
                    Path.Combine(path, fileName),
                    new UpdateFileRequest($"Update {fileName}",
                                          content,
                                          repositoryContent.Sha,
                                          "main")
                    );
        }

        public async Task<bool> ExistFileAsync(long repositoryId, string path, string fileName)
        {
            GhFileContent fileContent = await GetFileContentAsync(repositoryId, path, fileName);
            return fileContent != null;
        }

        public async Task<IEnumerable<GhFileContent>> GetAllFilesAsync(long repositoryId, string path)
        {
            try
            {
                IReadOnlyList<RepositoryContent> contents = await _githubClient.Repository.Content.GetAllContents(repositoryId, path);

                var files = contents.Where(x => x.Type == new StringEnum<ContentType>(ContentType.File)).Select(x => new GhFileContent()
                {
                    Name = x.Name,
                    Content = x.Content,
                    DownloadUrl = x.DownloadUrl
                });

                return files;
            }
            catch (Octokit.NotFoundException)
            {
                return null;
            }
        }

        public async Task<GhFileContent> GetFileContentAsync(long repositoryId, string path, string fileName)
        {
            try
            {
                IReadOnlyList<RepositoryContent> contents = await _githubClient.Repository.Content.GetAllContents(repositoryId, Path.Combine(path, fileName));
                RepositoryContent content = contents[0];
                return new GhFileContent()
                {
                    Name = content.Name,
                    Content = content.Content,
                    DownloadUrl = content.DownloadUrl
                };
            }
            catch (Octokit.NotFoundException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<GhFileContent>> GetAllFilesWithContentAsync(long repositoryId, string path)
        {
            try
            {
                IReadOnlyList<RepositoryContent> contents = await _githubClient.Repository.Content.GetAllContents(repositoryId, path);

                var files = contents.Where(x => x.Type == new StringEnum<ContentType>(ContentType.File)).Select(x => new GhFileContent()
                {
                    Name = x.Name,
                    Content = x.Content,
                    DownloadUrl = x.DownloadUrl
                }).ToList();

                for (int i = 0; i < files.Count; i++)
                {
                    var fileContent = await GetFileContentAsync(repositoryId, path, files[i].Name);

                    files[i].Content = fileContent.Content;
                }

                return files;
            }
            catch (Octokit.NotFoundException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<GhFileContent>> GetAllDirectoriesAsync(long repositoryId, string path)
        {
            try
            {
                IReadOnlyList<RepositoryContent> contents = await _githubClient.Repository.Content.GetAllContents(repositoryId, path);

                var files = contents.Where(x => x.Type == new StringEnum<ContentType>(ContentType.Dir)).Select(x => new GhFileContent()
                {
                    Name = x.Name,
                    Content = x.Content,
                    DownloadUrl = x.DownloadUrl
                });

                return files;
            }
            catch (Octokit.NotFoundException)
            {
                return null;
            }
        }
    }
}
