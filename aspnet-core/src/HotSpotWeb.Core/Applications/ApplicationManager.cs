using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace HotSpotWeb.Applications
{
	public class ApplicationManager : IApplicationManager
	{
        private readonly IRepository<Application, int> _applicationRepository;
        
        
		public ApplicationManager(IRepository<Application, int> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Application> GetAsync(int id)
        {
            var @application = await _applicationRepository.FirstOrDefaultAsync(id);
            
            if (@application == null)
            {
                throw new UserFriendlyException("Could not found the application, maybe it's deleted.");
            }

            return @application;
        }

        public async Task<List<Application>> GetAllAsync()
        {
            return await _applicationRepository.GetAllListAsync();
        }

        public async Task<Application> CreateAsync(Application application)
        {
            return await _applicationRepository.InsertAsync(application);
        }

        public Task DeleteAsync(int id)
        {
            var @application = _applicationRepository.DeleteAsync(id);
            
            if (@application == null)
            {
                throw new UserFriendlyException("Could not found the application, maybe it's deleted.");
            }
            
            return @application;
        }

        public Task<bool> StartApplication(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Application> UpdateAsync(Application application)
        {
            return _applicationRepository.UpdateAsync(application);
        }
    }
}

