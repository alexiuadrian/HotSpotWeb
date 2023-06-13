using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpotWeb.GithubProfiles;

namespace HotSpotWeb.Applications.Dtos
{
    public class CreateGithubRepositoryWithApplicationDto
    {
        public int ApplicationId { get; set; }
        public int GithubProfileId { get; set; }
    }
}
