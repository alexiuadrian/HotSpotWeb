using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using HotSpotWeb.Applications;
using HotSpotWeb.GithubRepositories;
using Newtonsoft.Json;

namespace HotSpotWeb
{
    public static class CommandsServiceHelper
    {
        public static async Task<string> SendCreateGithubRepository(GithubRepository githubRepository)
        {
            var payload = new
            {
                personal_token = githubRepository.GithubProfile.Token,
                repository_name = githubRepository.RepositoryName,
                user_name = githubRepository.GithubProfile.Username,
                description = githubRepository.Description
            };

            var httpClient = new HttpClient();
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:3000/create_repository.json", httpContent);

            // response is of type: {"message":"Repository created","url":"https://github.com/alexiuadrian/test-repo"}
            // we can parse it to get the url

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseContentObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var url = responseContentObject.url;
                return url;
            }
            else
            {
                Console.WriteLine($"Failed to post repository. Status code: {response.StatusCode}");
                return null;
            }
        }

        public static async Task SendGenerateAndUploadToGithub(GithubRepository githubRepository)
        {
            // generate the project
            await SendCreateApplication(githubRepository.Application);
            
            // add 5 seconds delay
            await Task.Delay(5000);
            
            // upload the project to github
            var payloadToGithub = new
            {
                personal_token = githubRepository.GithubProfile.Token,
                repository_name = githubRepository.RepositoryName,
                user_name = githubRepository.GithubProfile.Username,
                application_name = githubRepository.Application?.Name
            };
            
            var httpClient = new HttpClient();
            var jsonPayload = JsonConvert.SerializeObject(payloadToGithub);
            
            var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync("http://localhost:3000/upload", httpContent);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Successfully uploaded to github");
            }
            else
            {
                Console.WriteLine($"Failed to upload to github. Status code: {response.StatusCode}");
            }
        }
        
        public static async Task<bool> SendCreateApplication(Application application)
        {

            // check if application is null
            if (application == null)
            {
                throw new UserFriendlyException("Could not find the application, maybe it's deleted.");
            }

            // get configuration
            var configuration = application.Configuration;

            // check if application is null
            if (configuration == null)
            {
                throw new UserFriendlyException("Could not find the configuration, maybe it's deleted.");
            }

            application.Configuration = configuration;

            //// check if application is published
            //if (application.Status != "Published")
            //{
            //    throw new UserFriendlyException("Application is not published.");
            //}

            // // make a call to http://localhost:3000/
            // var payload = new
            // {
            //     name = "rails",
            //     is_available = true,
            //     requires_admin = false,
            //     flags = new[] { "new", "test-app", "--css=tailwind", "-f" }
            // };

            Payload.Payload payload = null;

            switch (application.Configuration.Language)
            {
                case "Ruby":
                {
                    switch (application.Configuration.Framework)
                    {
                        case "Rails":
                        {
                            string[] flags = { "new", application.Name, "--css=tailwind", "-f" };
                            payload = new Payload.Payload("rails", true, false, flags);
                            break;
                        }
                    }
                }
                break;
            }

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
    }
}
