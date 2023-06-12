using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.GithubProfiles
{
    public interface IGithubProfileManager : IDomainService
    {
        Task<GithubProfile> GetAsync(int id);
        Task<GithubProfile> CreateAsync(GithubProfile githubProfile);
        Task<GithubProfile> UpdateAsync(GithubProfile githubProfile);
        Task DeleteAsync(int id);
        Task<List<GithubProfile>> GetAllAsync();
    }
}
