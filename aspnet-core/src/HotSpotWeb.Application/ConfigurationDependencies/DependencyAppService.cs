using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpotWeb.ConfigurationDependencies.Dtos;
using HotSpotWeb.Dependencies;

namespace HotSpotWeb.ConfigurationDependencies
{
    public class DependencyAppService : HotSpotWebAppServiceBase, IDependencyAppService
    {
        private readonly IDependencyManager _dependencyManager;
        public DependencyAppService(IDependencyManager dependencyManager)
        {
            _dependencyManager = dependencyManager;
        }

        public async Task CreateAsync(CreateDependencyDto input)
        {
            var dependency = Dependency.Create(input.Name, input.Version, input.Type, input.OfficialUrl, input.TargetFramework);
            await _dependencyManager.CreateAsync(dependency);
        }

        public async Task DeleteAsync(int id)
        {
            var dependency = await _dependencyManager.GetAsync(id);

            if (dependency != null)
            {
                await _dependencyManager.DeleteAsync(id);
            } else {
                throw new Abp.UI.UserFriendlyException("Could not find the dependency, maybe it's already deleted.");
            }
        }

        public async Task<Dependency> GetAsync(int id)
        {
            return await _dependencyManager.GetAsync(id);
        }

        public Task<List<Dependency>> GetListAsync(GetDependenciesListInput input)
        {
            return _dependencyManager.GetAllAsync();
        }
    }
}
