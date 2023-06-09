using System.Collections.Generic;
using HotSpotWeb.Applications;
using HotSpotWeb.Dependencies;

namespace HotSpotWeb.Configurations.Dtos;

public class CreateConfigurationDto
{
    public string Name { get; set; }
    public string Language { get; set; }
    public string Framework { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public List<Dependency> Dependencies { get; set; }
    public Application Application { get; set; }
}