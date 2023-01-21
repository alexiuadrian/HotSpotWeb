using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HotSpotWeb.Applications;
using HotSpotWeb.Dependencies;

namespace HotSpotWeb.Configurations
{
    [Table("Configurations")]
    public class Configuration : Entity, IHasCreationTime, IHasModificationTime
    {
        public const int MaxNameLength = 128;
        public const int MaxDescriptionLength = 2048;

        public Configuration()
        {

        }
        public Configuration(string name = null, string language = null, string framework = null, string version = null, string description = null, long userId = 0, List<Dependency> dependencies = null, Application application = null, DateTime creationTime = default, DateTime? lastModificationTime = default)
        {
            Name = name;
            Language = language;
            Framework = framework;
            Version = version;
            Description = description;
            UserId = userId;
            Dependencies = dependencies;
            Application = application;
            CreationTime = creationTime;
            LastModificationTime = lastModificationTime;
        }

        [Required]
        [StringLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Framework { get; set; }
        
        public string Version { get; set; }

        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public long UserId { get; set; }

        public List<Dependency> Dependencies { get; set; }

        public Application Application { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public static Configuration Create(string name, string language, string framework, string version, string description, long userId, List<Dependency> dependencies, Application application)
        {
            var configuration = new Configuration(name, language, framework, version, description, userId, dependencies, application, DateTime.Now, null);

            return configuration;
        }
    }
}
