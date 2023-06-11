using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace HotSpotWeb.Dependencies
{
    public class DependencyManager : IDependencyManager
    {
        private readonly IRepository<Dependency> _dependencyRepository;
        public DependencyManager(IRepository<Dependency> dependencyRepository)
        {
            _dependencyRepository = dependencyRepository;
        }

        public Task CreateAsync(Dependency dependency)
        {
            var existingDependency = _dependencyRepository.FirstOrDefault(d => d.Name == dependency.Name);

            if (existingDependency != null)
            {
                throw new Abp.UI.UserFriendlyException("The dependency already exists.");
            } else {
                _dependencyRepository.Insert(dependency);
                return Task.FromResult(0);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var dependency = await _dependencyRepository.GetAsync(id);
            if (dependency != null)
            {
                _dependencyRepository.Delete(dependency);
            } else {
                throw new Abp.UI.UserFriendlyException("Could not find the dependency, maybe it's already deleted.");
            }
        }

        public Task<List<Dependency>> GetAllAsync()
        {
            return _dependencyRepository.GetAllListAsync();
        }

        public Task<Dependency> GetAsync(int id)
        {
            return _dependencyRepository.GetAsync(id);
        }

        public Task<Dependency> UpdateAsync(Dependency dependency)
        {
            throw new NotImplementedException();
        }
    }
}
