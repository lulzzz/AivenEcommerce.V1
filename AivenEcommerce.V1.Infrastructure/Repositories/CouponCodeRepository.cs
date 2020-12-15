using AivenEcommerce.V1.Domain.Caching;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class CouponCodeRepository : GitHubRepository<CouponCode, Guid>, ICouponCodeRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public CouponCodeRepository(IGitHubService githubService, IGitHubOptions options, ICachedRepository cachedRepository) : base(githubService, options.CouponCodeRepositoryId, "couponcodes")
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task<CouponCode> GetCouponAsync(string code)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(CouponCode), nameof(GetCouponAsync), code),

                      async () =>
                      {
                          var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, code);

                          if (fileContent is null)
                          {
                              return null;
                          }

                          return fileContent.Content.Deserialize<CouponCode>();

                      }
                    );
        }

        public override async Task<CouponCode> CreateAsync(CouponCode entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.Code, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(CouponCode entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.Code, entityIn.Serialize());
        }

        public override Task RemoveAsync(CouponCode entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.Code);
        }
    }
}
