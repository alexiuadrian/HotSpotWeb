using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using HotSpotWeb.Configurations;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace HotSpotWeb.Applications
{
	public class ApplicationManager : IApplicationManager
	{
        private readonly IRepository<Application, int> _applicationRepository;
        private readonly IRepository<Configurations.Configuration, int> _configurationRepository;
                
		public ApplicationManager(IRepository<Application, int> applicationRepository, IRepository<Configurations.Configuration, int> configurationRepository)
        {
            _applicationRepository = applicationRepository;
            _configurationRepository = configurationRepository;
        }

        public async Task<Application> GetAsync(int id)
        {
            var @application = await _applicationRepository.FirstOrDefaultAsync(id);
            
            if (@application == null)
            {
                throw new UserFriendlyException("Could not find the application, maybe it's deleted.");
            }

            var configuration = await _configurationRepository.FirstOrDefaultAsync(@application.ConfigurationId);

            if (configuration == null)
            {
                throw new UserFriendlyException("Could not find the configuration associated with this application.");
            }

            application.Configuration = configuration;

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
                throw new UserFriendlyException("Could not find the application, maybe it's deleted.");
            }
            
            return @application;
        }

        public async Task<bool> StartApplication(int id)
        {
            // get application
            var application = await _applicationRepository.FirstOrDefaultAsync(id);

            // check if application is null
            if (application == null)
            {
                throw new UserFriendlyException("Could not find the application, maybe it's deleted.");
            }

            return await CommandsServiceHelper.SendCreateApplication(application);
        }

        public Task<Application> UpdateAsync(Application application)
        {
            return _applicationRepository.UpdateAsync(application);
        }
    }
}

