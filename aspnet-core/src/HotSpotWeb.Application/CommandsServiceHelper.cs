using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotSpotWeb.GithubRepositories;
using Newtonsoft.Json;

namespace HotSpotWeb
{
    public static class CommandsServiceHelper
    {
        public static async void SendCreateGithubRepository(GithubRepository githubRepository)
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

            var response = await httpClient.PostAsync("http://localhost:3000/repositories/create.json", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Repository posted successfully");
            }
            else
            {
                Console.WriteLine($"Failed to post repository. Status code: {response.StatusCode}");
            }
        }
    }
}
