using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HotSpotWeb.Applications;
using HotSpotWeb.GithubProfiles;
using JetBrains.Annotations;

namespace HotSpotWeb.GithubRepositories
{
    [Table("GithubRepositories")]
    public class GithubRepository : Entity, IHasCreationTime, IHasModificationTime
    {
        public GithubRepository()
        {

        }

        public GithubRepository(string repositoryName = null, string description = null, GithubProfile githubProfile = null,
            Application application = null)
        {
            RepositoryName = repositoryName;
            Description = description;
            GithubProfile = githubProfile;
            Application = application;
        }
        
        public string RepositoryName { get; set; }
        public string Description { get; set; }
        public int GithubProfileId { get; set; }
        public GithubProfile GithubProfile { get; set; }
        public int ApplicationId { get; set; }
        [CanBeNull] public Application Application { get; set; }
        public bool IsApplicationOnRepository { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public static GithubRepository Create(string repositoryName, string description, GithubProfile githubProfile, Application application)
        {
            return new GithubRepository(repositoryName, description, githubProfile, application);
        }
    }
}
