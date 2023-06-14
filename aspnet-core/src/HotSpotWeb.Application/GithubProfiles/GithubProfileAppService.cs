using Abp.Runtime.Session;
using HotSpotWeb.GithubProfiles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.GithubProfiles
{
    public class GithubProfileAppService : HotSpotWebAppServiceBase, IGithubProfileAppService
    {
        private readonly IGithubProfileManager _githubProfileManager;

        public GithubProfileAppService(IGithubProfileManager githubProfileManager)
        {
            _githubProfileManager = githubProfileManager;
        }
        
        public Task CreateAsync(CreateGithubProfileDto input)
        {
            var githubProfile = GithubProfile.Create(input.Username, input.Token, input.Description, AbpSession.GetUserId());
            return _githubProfileManager.CreateAsync(githubProfile);
        }

        public Task DeleteAsync(int id)
        {
            return _githubProfileManager.DeleteAsync(id);
        }

        public Task<GithubProfile> GetAsync(int id)
        {
            return _githubProfileManager.GetAsync(id);
        }

        public async Task<List<GithubProfile>> GetListAsync(GetGithubProfilesListInput input)
        {
            return await _githubProfileManager.GetAllAsync();
        }
    }
}
