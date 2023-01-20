﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using HotSpotWeb.Dependencies;

namespace HotSpotWeb.Applications.Dtos
{
    [AutoMapFrom(typeof(Application))]
    public class ApplicationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string VersionControl { get; set; }
        public string RepositoryUrl { get; set; }
        public string RepositoryUsername { get; set; }
        public string RepositoryBranch { get; set; }
        public string Technology { get; set; }
        public List<Dependency> Dependencies { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long UserId { get; set; }
    }
}