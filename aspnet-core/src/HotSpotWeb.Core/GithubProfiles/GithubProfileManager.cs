using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace HotSpotWeb.GithubProfiles
{
    public class GithubProfileManager : IGithubProfileManager
    {
        private readonly IRepository<GithubProfile> _githubProfileRepository;
        public GithubProfileManager(IRepository<GithubProfile> githubProfileRepository)
        {
            _githubProfileRepository = githubProfileRepository;
        }

        public Task<GithubProfile> CreateAsync(GithubProfile githubProfile)
        {
            return _githubProfileRepository.InsertAsync(githubProfile);
        }

        public async Task DeleteAsync(int id)
        {
            var @githubProfile = await _githubProfileRepository.GetAsync(id);
            if (@githubProfile != null)
            {
                await _githubProfileRepository.DeleteAsync(id);
            } else {
                throw new UserFriendlyException("Could not find the Github profile, maybe it's deleted.");
            }
        }

        public Task<List<GithubProfile>> GetAllAsync()
        {
            return _githubProfileRepository.GetAllListAsync();
        }

        public Task<GithubProfile> GetAsync(int id)
        {
            var @githubProfile = _githubProfileRepository.GetAsync(id);
            if (@githubProfile != null)
            {
                return @githubProfile;
            } else {
                throw new UserFriendlyException("Could not find the Github profile, maybe it's deleted.");
            }
        }

        public Task<GithubProfile> UpdateAsync(GithubProfile githubProfile)
        {
            var @githubProfileInDb = _githubProfileRepository.GetAsync(githubProfile.Id);
            if (@githubProfileInDb != null)
            {
                return _githubProfileRepository.UpdateAsync(githubProfile);
            } else {
                throw new UserFriendlyException("Could not find the Github profile, maybe it's deleted.");
            }
        }
    }
}
