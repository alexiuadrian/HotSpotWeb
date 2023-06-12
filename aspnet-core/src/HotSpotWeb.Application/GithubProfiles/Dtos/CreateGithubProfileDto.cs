using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.GithubProfiles.Dtos
{
    public class CreateGithubProfileDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
