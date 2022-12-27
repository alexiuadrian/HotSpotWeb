using Abp.Domain.Entities;

namespace HotSpotWeb.Dependencies;

public class Dependency : Entity
{
    public string Name { get; set; }
    public string Version { get; set; }
    public string Type { get; set; }
    public string OfficialUrl { get; set; }
    public string TargetFramework { get; set; }
}