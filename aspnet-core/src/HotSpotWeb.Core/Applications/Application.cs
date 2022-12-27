using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;
using HotSpotWeb.Dependencies;
using JetBrains.Annotations;

namespace HotSpotWeb.Applications
{
	[Table("Applications")]
	public class Application : Entity, IHasCreationTime, IHasModificationTime, IMustHaveTenant
	{
		public const int MaxNameLength = 128;
		public const int MaxDescriptionLength = 2048;

		[Required]
		[StringLength(MaxNameLength)]
		public string Name { get; set; }

		[StringLength(MaxDescriptionLength)]
		public string Description { get; set; }

		[CanBeNull]
		public string Version { get; set; }

		public string Type { get; set; } // frontend/backend

		public string Status { get; set; } // active/disabled

		[CanBeNull] 
		public string Url { get; set; }

		[CanBeNull]
		public string Icon { get; set; }
		
		public string Color { get; set; }
		
		public string VersionControl { get; set; } // git/svn
		
		public string RepositoryUrl { get; set; }
		
		public string RepositoryUsername { get; set; }
		
		public string RepositoryBranch { get; set; }
		
		public string Technology { get; set; } // .net/java/php/angular/react

		public List<Dependency> Dependencies { get; set; } // list of dependencies
		public DateTime CreationTime { get; set; }

		public DateTime? LastModificationTime { get; set; }
		
		public int TenantId { get; set; }

		public static Application Create (string name, string description, string version, string type, string status, string url, string icon, string color, string versionControl, string repositoryUrl, string repositoryUsername, string repositoryBranch, string technology)
		{
			var application = new Application
			{
				Name = name,
				Description = description,
				Version = version,
				Type = type,
				Status = status,
				Url = url,
				Icon = icon,
				Color = color,
				VersionControl = versionControl,
				RepositoryUrl = repositoryUrl,
				RepositoryUsername = repositoryUsername,
				RepositoryBranch = repositoryBranch,
				Technology = technology,
				CreationTime = Clock.Now
			};
			
			application.Dependencies = new List<Dependency>();

			return application;
		}

		public override string ToString()
		{
			return string.Format("[Application {0}] {1}", Id, Name);
		}
	}
}

