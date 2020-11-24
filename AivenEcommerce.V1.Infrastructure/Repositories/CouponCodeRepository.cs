using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class CouponCodeRepository : GitHubRepository<CouponCode, Guid>, ICouponCodeRepository
    {
        public CouponCodeRepository(IGitHubService githubService, IGitHubOptions options) : base(githubService, options.CouponCodeRepositoryId, "couponcodes")
        {
        }

        public async Task<CouponCode> GetCouponAsync(string code)
        {
            var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, code);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<CouponCode>();
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
