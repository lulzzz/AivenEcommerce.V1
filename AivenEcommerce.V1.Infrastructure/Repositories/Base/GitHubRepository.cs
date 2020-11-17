using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities.Base;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Modules.GitHub.Services;

namespace AivenEcommerce.V1.Infrastructure.Repositories.Base
{
    public class GitHubRepository<T, K> : IRepository<T, K> where T : IEntity<K> where K : new()
    {
        protected readonly IGitHubService _githubService;

        public GitHubRepository(IGitHubService githubService)
        {
            _githubService = githubService ?? throw new ArgumentNullException(nameof(githubService));
        }

        public virtual Task<T> CreateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            throw new System.NotImplementedException();
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

        public Task RemoveAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}