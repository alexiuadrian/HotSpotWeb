using System;
using System.Collections.Generic;
using Abp.AutoMapper;
using HotSpotWeb.Applications;
using HotSpotWeb.Dependencies;

namespace HotSpotWeb.Configurations.Dtos;

[AutoMapFrom(typeof(Configuration))]
public class ConfigurationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Language { get; set; }
    public string Framework { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public long UserId { get; set; }
    public List<Dependency> Dependencies { get; set; }
    public Application Application { get; set; }
    public DateTime CreationTime { get; set; }
}