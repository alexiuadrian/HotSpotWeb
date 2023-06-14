using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using HotSpotWeb.Applications;
using HotSpotWeb.GithubProfiles;
using Microsoft.Extensions.Logging;

namespace HotSpotWeb.GithubRepositories
{
	public class GithubRepositoryManager : IGithubRepositoryManager
	{
        private readonly IRepository<GithubRepository, int> _githubRepositoryRepository;
        private readonly IApplicationManager _applicationManager;
        private readonly IGithubProfileManager _githubProfileManager;
        public GithubRepositoryManager(IRepository<GithubRepository, int> githubRepositoryRepository, IApplicationManager applicationManager,
            IGithubProfileManager githubProfileManager)
		{
            _githubRepositoryRepository = githubRepositoryRepository;
            _applicationManager = applicationManager;
            _githubProfileManager = githubProfileManager;
		}

        public Task<GithubRepository> CreateAsync(GithubRepository githubRepository)
        {
            return _githubRepositoryRepository.InsertAsync(githubRepository);
        }

        public Task DeleteAsync(int id)
        {
            var githubRepositoryInDb = _githubRepositoryRepository.GetAsync(id);

            if (githubRepositoryInDb == null)
            {
                throw new UserFriendlyException("Could not find the Github repository, maybe it's deleted.");
            }

            return _githubRepositoryRepository.DeleteAsync(id);
        }

        public async Task<List<GithubRepository>> GetAllAsync()
        {
            return await _githubRepositoryRepository.GetAllListAsync();
        }

        public async Task<GithubRepository> GetAsync(int id)
        {
            var @githubRepository = await _githubRepositoryRepository.GetAsync(id);

            if (githubRepository == null)
            {
                throw new UserFriendlyException("Could not find the Github repository, maybe it's deleted.");
            }
            
            var application = await _applicationManager.GetAsync(@githubRepository.ApplicationId);
            
            if (application == null)
            {
                throw new UserFriendlyException("Could not find the application associated with this Github repository.");
            }
            
            githubRepository.Application = application;
            
            var githubProfile = await _githubProfileManager.GetAsync(@githubRepository.GithubProfileId);
            
            if (githubProfile == null)
            {
                throw new UserFriendlyException("Could not find the Github profile associated with this Github repository.");
            }
            
            githubRepository.GithubProfile = githubProfile;

            return @githubRepository;
        }

        public async Task<GithubRepository> UpdateAsync(GithubRepository githubRepository)
        {
            var @githubRepositoryInDb = await _githubRepositoryRepository.GetAsync(githubRepository.Id);

            if (githubRepository == null)
            {
                throw new UserFriendlyException("Could not find the Github repository, maybe it's deleted.");
            }

            return await _githubRepositoryRepository.UpdateAsync(githubRepository);
        }

        public async Task<int> IsApplicationOnGithub(int applicationId)
        {
            var githubRepository = await _githubRepositoryRepository.FirstOrDefaultAsync(x => x.ApplicationId == applicationId);

            int result = 0;
            
            if (githubRepository != null)
            {
                if (githubRepository.IsApplicationOnRepository)
                {
                    result = 2;
                }
                else
                {
                    result = 1;
                }
            }

            return result;
        }

        public async Task<GithubRepository> findGithubRepositoryForApplicationId(Application application)
        {
            var githubRepository = await _githubRepositoryRepository.FirstOrDefaultAsync(x => x.ApplicationId == application.Id);

            if (githubRepository == null)
            {
                throw new UserFriendlyException("Could not find the Github repository, maybe it's deleted.");
            }

            return githubRepository;
        }
    }
}

