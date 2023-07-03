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

        public static async Task UploadToGithub(GithubRepository githubRepository)
        {
            // upload the project to github
            var payloadToGithub = new
            {
                personal_token = githubRepository.GithubProfile.Token,
                repository_name = githubRepository.RepositoryName,
                user_name = githubRepository.GithubProfile.Username,
                application_name = githubRepository.Application?.Name,
                local_path = githubRepository.Application?.LocalPath
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
        
        public static async Task<string> SendCreateApplication(Application application)
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

            //switch (application.Configuration.Language)
            //{
            //    case "Ruby":
            //    {
            //        switch (application.Configuration.Framework)
            //        {
            //            case "Rails":
            //            {
            //                string[] flags = { "new", application.Name, "--css=tailwind", "-f" };
            //                payload = new Payload.Payload("rails", true, false, flags);
            //                break;
            //            }
            //            default:
            //                break;
            //        }
            //        break;
            //    }
            //    case "Javascript":
            //    {
            //        switch (application.Configuration.Framework)
            //        {
            //            case "React":
            //                {
            //                    string[] flags = { "create-react-app", application.Name };
            //                    payload = new Payload.Payload("npx", true, false, flags);
            //                    break;
            //                }
            //            default:
            //                break;
            //        }
            //        break;
            //    }
            //    case "Default":
            //    {
            //        break;
            //    }
            //}

            switch (application.Configuration.Language)
            {
                case "Ruby":
                    {
                        switch (application.Configuration.Framework)
                        {
                            case "Ruby on Rails":
                                {
                                    string[] flags = { "new", application.Name, "--css=tailwind", "-f" };
                                    payload = new Payload.Payload("rails", true, false, flags);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case "Javascript":
                    {
                        switch (application.Configuration.Framework)
                        {
                            case "React":
                                {
                                    string[] flags = { "create-react-app", application.Name };
                                    payload = new Payload.Payload("npx", true, false, flags);
                                    break;
                                }
                            case "Angular":
                                {
                                    string[] flags = { "new", application.Name, "--defaults" };
                                    payload = new Payload.Payload("ng", true, false, flags);
                                    break;
                                }
                            case "Vue":
                                {
                                    string[] flags = { "create", application.Name };
                                    payload = new Payload.Payload("vue", true, false, flags);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case "Python":
                    {
                        switch (application.Configuration.Framework)
                        {
                            case "Django":
                                {
                                    string[] flags = { "startproject", application.Name };
                                    payload = new Payload.Payload("django-admin", true, false, flags);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case "PHP":
                    {
                        switch (application.Configuration.Framework)
                        {
                            case "Laravel":
                                {
                                    string[] flags = { "create-project", "--prefer-dist", "laravel/laravel", application.Name };
                                    payload = new Payload.Payload("composer", true, false, flags);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case "Java":
                    {
                        switch (application.Configuration.Framework)
                        {
                            case "Spring":
                                {
                                    string[] flags = { "init", application.Name };
                                    payload = new Payload.Payload("spring", true, false, flags);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case "C#":
                    {
                        switch (application.Configuration.Framework)
                        {
                            case ".NET Core":
                                {
                                    string[] flags = { "new", "console", "-o", application.Name };
                                    payload = new Payload.Payload("dotnet", true, false, flags);
                                    break;
                                }
                            case "ASP.NET":
                                {
                                    string[] flags = { "new", "webapp", "-n", application.Name };
                                    payload = new Payload.Payload("dotnet", true, false, flags);
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    {
                        // Default case if the language is not found
                        break;
                    }
            }


            var httpClient = new HttpClient();
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:3000/commands.json", httpContent);

            // retrieve the path
            // response is of type: {"path":"/Users/adialexiu/Desktop/cmd_results/1686826286/"}

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseContentObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var path = responseContentObject.path;
                return path;
            }
            else
            {
                Console.WriteLine($"Failed to post command. Status code: {response.StatusCode}");
                return null;
            }
        }
        
        public static async Task<string> UploadToAzure(Application application)
        {
            // upload the project to azure
            var payload = new
            {
                local_path = application.LocalPath,
                file_name = application.Name
            };
            
            var httpClient = new HttpClient();
            var jsonPayload = JsonConvert.SerializeObject(payload);
            
            var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync("http://localhost:3000/upload_file", httpContent);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseContentObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var url = responseContentObject?.url;
                return url;
            }
            else
            {
                Console.WriteLine($"Failed to upload to github. Status code: {response.StatusCode}");
                return null;
            }
        }
    }
}
