using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.GitHub;
using AivenEcommerce.V1.Domain.Repositories;

using Octokit;

namespace AivenEcommerce.V1.Infrastructure.Repositories.Base
{
    public class GitHubRepository<T> : IRepository<T> where T : IEntity<string>
    {
        private readonly IGitHubClient _githubClient;

        public GitHubRepository(IGitHubClient githubClient)
        {
            _githubClient = githubClient;
        }

        #region ProtectedMethods
        protected async Task CreateFileAsync(long repositoryId, string path, string fileName, string content)
        {
            await _githubClient.Repository.Content.CreateFile(
                    repositoryId,
                    Path.Combine(path, fileName),
                    new CreateFileRequest($"Create {fileName}",
                                          content,
                                          "main"));
        }

        protected async Task DeleteFileAsync(long repositoryId, string path, string fileName)
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

        protected async Task UpdateFileAsync(long repositoryId, string path, string fileName, string content)
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

        protected async Task<bool> ExistFileAsync(long repositoryId, string path, string fileName)
        {
            GhFileContent fileContent = await GetFileContentAsync(repositoryId, path, fileName);
            return fileContent != null;
        }

        protected async Task<IEnumerable<GhFileContent>> GetAllFilesAsync(long repositoryId, string path)
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

        protected async Task<GhFileContent> GetFileContentAsync(long repositoryId, string path, string fileName)
        {
            try
            {
                IReadOnlyList<RepositoryContent> contents = await _githubClient.Repository.Content.GetAllContents(repositoryId, Path.Combine(path, fileName));
                RepositoryContent content = contents.First();
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

        protected async Task<IEnumerable<GhFileContent>> GetAllFilesWithContentAsync(long repositoryId, string path)
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

        protected async Task<IEnumerable<GhFileContent>> GetAllDirectoriesAsync(long repositoryId, string path)
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
        #endregion

        public virtual Task<T> CreateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> GetAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task RemoveAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task RemoveAsync(T entityIn)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task UpdateAsync(T entityIn)
        {
            throw new System.NotImplementedException();
        }
    }
}