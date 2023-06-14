using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.GithubRepositories.Dtos
{
    public class CreateGithubRepositoryDto
    {
        public string RepositoryName { get; set; }
        public string Description { get; set; }
        public int GithubProfileId { get; set; }
        public int ApplicationId { get; set; }
    }
}
