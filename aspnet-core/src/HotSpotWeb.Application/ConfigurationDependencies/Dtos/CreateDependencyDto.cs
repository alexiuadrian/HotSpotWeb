using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.ConfigurationDependencies.Dtos
{
    public class CreateDependencyDto
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public string OfficialUrl { get; set; }
        public string TargetFramework { get; set; }
    }
}
