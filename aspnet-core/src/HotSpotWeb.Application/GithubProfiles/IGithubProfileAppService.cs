using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpotWeb.GithubProfiles.Dtos;

namespace HotSpotWeb.GithubProfiles
{
    public interface IGithubProfileAppService : IApplicationService
    {
        Task<GithubProfile> GetAsync(int id);
        Task<List<GithubProfile>> GetListAsync(GetGithubProfilesListInput input);
        Task CreateAsync(CreateGithubProfileDto input);
        Task DeleteAsync(int id);
    }
}
