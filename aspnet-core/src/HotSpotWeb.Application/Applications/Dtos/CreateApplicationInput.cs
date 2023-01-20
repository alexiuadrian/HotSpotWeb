using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Abp.AutoMapper;
using HotSpotWeb.Dependencies;

namespace HotSpotWeb.Applications.Dtos;

public class CreateApplicationInput
{
    [Required]
    [StringLength(Application.MaxNameLength)]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [Required]
    public string Status { get; set; }
    
    [Required]
    public string Version { get; set; }
    
    [Required]
    public string Type { get; set; }
    
    public string Url { get; set; }
    
    public string Icon { get; set; }
    
    public string Color { get; set; }

    public string VersionControl { get; set; }
    
    public string RepositoryUrl { get; set; }
    
    public string RepositoryUsername { get; set; }
    
    public string RepositoryBranch { get; set; }
    
    [Required]
    public string Technology { get; set; }

    public List<Dependency> Dependencies { get; set; }

    [Required]
    public long UserId { get; set; }
}