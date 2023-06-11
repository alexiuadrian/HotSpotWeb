using System;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotSpotWeb.Dependencies;

[Table("Dependencies")]
public class Dependency : Entity
{
    public Dependency()
    {

    }
    public Dependency(string name, string version, string type, string officialUrl, string targetFramework)
    {
        Name = name;
        Version = version;
        Type = type;
        OfficialUrl = officialUrl;
        TargetFramework = targetFramework;
    }

    public string Name { get; set; }
    public string Version { get; set; }
    public string Type { get; set; } // library, gem, etc.
    public string OfficialUrl { get; set; }
    public string TargetFramework { get; set; }

    public static Dependency Create(string name, string version, string type, string officialUrl, string targetFramework)
    {
        return new Dependency(name, version, type, officialUrl, targetFramework);
    }
}