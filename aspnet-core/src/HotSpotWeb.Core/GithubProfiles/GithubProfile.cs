using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HotSpotWeb.GithubProfiles
{
    [Table("GithubProfiles")]
    public class GithubProfile : Entity
    {
        public GithubProfile()
        {
        }
        public GithubProfile(string username, string token, string description, long userId)
        {
            Username = username;
            Token = token;
            Description = description;
            UserId = userId;
        }

        public string Username { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }

        public static GithubProfile Create(string username, string token, string description, long userId)
        {
            return new GithubProfile(username, token, description, userId);
        }
    }
}
