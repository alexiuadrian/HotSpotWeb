using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using HotSpotWeb.GithubProfiles;

namespace HotSpotWeb.GithubRepositories
{
    [Table("GithubRepositories")]
    public class GithubRepository : Entity
    {
        public GithubRepository()
        {

        }

        public GithubRepository(string repositoryName, string description, GithubProfile githubProfile)
        {
            RepositoryName = repositoryName;
            Description = description;
            GithubProfile = githubProfile;
        }
        
        public string RepositoryName { get; set; }
        public string Description { get; set; }
        public int GithubProfileId { get; set; }
        public GithubProfile GithubProfile { get; set; }

        public static GithubRepository Create(string repositoryName, string description, GithubProfile githubProfile)
        {
            return new GithubRepository(repositoryName, description, githubProfile);
        }
    }
}
