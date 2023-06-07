using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

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
                throw new UserFriendlyException("Could not find the application, maybe it's deleted.");
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
                throw new UserFriendlyException("Could not find the application, maybe it's deleted.");
            }
            
            return @application;
        }

        public async Task<bool> StartApplication(int id)
        {
            // get application
            // var application = _applicationRepository.FirstOrDefaultAsync(id);
            //
            // // check if application is null
            // if (application == null)
            // {
            //     throw new UserFriendlyException("Could not find the application, maybe it's deleted.");
            // }
            //
            // // check if application is published
            // if (application.Result.Status != "Published")
            // {
            //     throw new UserFriendlyException("Application is not published.");
            // }
            
            // make a call to http://localhost:3000/
            var payload = new
            {
                name = "rails",
                is_available = true,
                requires_admin = false,
                flags = new[] { "new", "test-app", "--css=tailwind", "-f" }
            };

            var httpClient = new HttpClient();
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:3000/commands.json", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Command posted successfully");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to post command. Status code: {response.StatusCode}");
                return false;
            }
        }

        public Task<Application> UpdateAsync(Application application)
        {
            return _applicationRepository.UpdateAsync(application);
        }

        public Task<HashSet<Application>> GetByConfigurationId(int id)
        {
            HashSet<Application> result = new HashSet<Application>();
            var applications = _applicationRepository.GetAllListAsync();

            foreach (var application in applications.Result)
            {
                var applicationConfigurations = application.Configurations?.Where(x => x.Id == id);
                if (applicationConfigurations != null)
                {
                    result.Add(application);
                }
            }

            return Task.FromResult(result);
        }
    }
}

