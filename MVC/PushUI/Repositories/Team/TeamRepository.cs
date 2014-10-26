using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using Concord.Push.Service.Team;
using PushUI.Models.Application.Config;
using PushUI.Models.Team;
using Environment = Concord.Push.Models.Source.Environment;
using Branch = Concord.Push.Models.Source.Branch;
using MergeEnvironment = Concord.Push.Models.Source.MergeEnvironment;
using BLL = Concord.Push.Business.Team;

namespace PushUI.Repositories.Team
{
	/// <summary>
	/// repository class to perform functions related to team data.
	/// </summary>
	public class TeamRepository : ITeamRepository
	{
		public ITeamDataAccess TeamDataAccess { get; set; }

		/// <summary>
		/// default constructor (passes in dataaccess layer dependency)
		/// </summary>
		public TeamRepository(ITeamDataAccess teamDataAccess)
		{
			TeamDataAccess = teamDataAccess;
		}

		public IServiceProvider GetTeamProjectCollection()
		{
			return BLL.Team.GetTeamProjectCollection(TeamDataAccess);
		}

		public TeamProject GetDefaultProject()
		{
			var projects = GetProjects().ToList();
			return projects.Find(p => p.IsDefault) ?? projects.FirstOrDefault();
		}

		public IEnumerable<TeamProject> GetProjects()
		{
			// only return projects configured for this website. todo: Cache this in application scope? GetSection is cached by default but should I cache the rest?
			var projectConfig = ConfigurationManager.GetSection("projectsConfig") as ProjectsConfigurationSection;

			if (projectConfig == null || projectConfig.Projects == null || projectConfig.Projects.Count <= 0)
				return null;

			var config = from ProjectConfigurationElement p in projectConfig.Projects
						 select p;
			return config.Select(project => new TeamProject
			{
				Name = project.Id,
				IsDefault = project.IsDefault,
				MergeEnvironments = (from MergeEnvironmentConfigurationElement m in project.MergeEnvironments select m).Select(environment => new MergeEnvironment
				{
					Name = environment.Id,
					Source = (Environment)Enum.Parse(typeof(Environment), environment.Source.Id, true),
					Target = (Environment)Enum.Parse(typeof(Environment), environment.Target.Id, true),
					AllowedStates = (from WorkItemStateConfigurationElement s in environment.AllowedStates select s).Select(state => state.Id),
					Relationships = (from BranchConfigurationElement b in environment.Branches select b).Select(branch => new MergeRelationship
					{
						Source = new Branch { Name = branch.Source.Id },
						Target = new Branch { Name = branch.Target.Id }
					})
				})
			}).ToList();
		}

		public TeamProject GetProject(string name)
		{
			return GetProjects().ToList()
				.FirstOrDefault(p => string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase) == 0);
		}

		public JsonTeamProject GetJsonProject(string name)
		{
			return new JsonTeamProject(GetProject(name));
		}

		public IEnumerable<JsonTeamProject> GetJsonProjects()
		{
			return GetProjects().Select(p => new JsonTeamProject(p)).ToList();
		}
	}
}