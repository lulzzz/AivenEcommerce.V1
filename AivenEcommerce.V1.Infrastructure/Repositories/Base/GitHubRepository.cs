using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Modules.GitHub.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Infrastructure.Repositories.Base
{
    public class GitHubRepository<T, K> : IRepository<T, K> where T : IEntity<K> where K : new()
    {
        protected IGitHubService GithubService { get; private set; }
        protected long RepositoryId { get; private set; }
        protected string Path { get; private set; }

        public GitHubRepository(IGitHubService githubService, long repositoryId)
        {
            GithubService = githubService ?? throw new ArgumentNullException(nameof(githubService));
            RepositoryId = repositoryId;
        }

        public GitHubRepository(IGitHubService githubService, long repositoryId, string path)
        {
            GithubService = githubService ?? throw new ArgumentNullException(nameof(githubService));
            Path = path ?? throw new ArgumentNullException(nameof(path));
            RepositoryId = repositoryId;
        }

        public virtual Task<T> CreateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var files = await GithubService.GetAllFilesWithContentAsync(RepositoryId, Path);

            if (files is null)
            {
                return Enumerable.Empty<T>();
            }

            return files.Select(x => x.Content.Deserialize<T>());
        }

        public virtual Task<T> GetAsync(K id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task RemoveAsync(K id)
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

        public async Task RemoveAllAsync()
        {
            var directories = await GithubService.GetAllDirectoriesAsync(RepositoryId, Path);

            foreach (var dir in directories)
            {
                var files = await GithubService.GetAllFilesAsync(RepositoryId, System.IO.Path.Combine(Path, dir.Name));
                foreach (var file in files)
                {
                    await GithubService.DeleteFileAsync(RepositoryId, Path, file.Name);
                }
            }


        }
    }
}