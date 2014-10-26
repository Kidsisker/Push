using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Concord.Push.Models.Team;
using Concord.Push.Service.Team;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsTeamProject = Microsoft.TeamFoundation.VersionControl.Client.TeamProject;

namespace Concord.Push.Data.Team
{
	/// <summary>
	/// data access class to get source data from the Tfs API 
	/// </summary>
	public class TfsTeamDataAccess : ITeamDataAccess
	{
		/// <summary>
		/// gets source levels for the specified account
		/// </summary>
		public IServiceProvider GetTeamProjectCollection()
		{
			return GetTfsTeamProjectCollection();
		}

		public IEnumerable<Project> GetProjects()
		{
			return GetTfsProjects().Select(MapProject).ToList();
		}

		public IIdentity GetAuthorizedIdentity()
		{
			var tfs = GetTfsTeamProjectCollection();
			return MapIdentity(tfs.AuthorizedIdentity);
		}

		internal static IEnumerable<TfsTeamProject> GetTfsProjects()
		{
			var tfs = GetTfsTeamProjectCollection();
			var projects = tfs.GetService<VersionControlServer>().GetAllTeamProjects(false).ToList();
			// todo: Add excludes list to config or see if Josh can remove or at least rename with "_obsolete" or something
			return projects.Where(p => p.Name != "Shared" && p.Name != "External Interlink" && p.Name != "Database Phoenix" && p.Name != "InterlinkAPI").ToList();
		}

		internal static TfsTeamProjectCollection GetTfsTeamProjectCollection()
		{
			return TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(ConfigurationManager.ConnectionStrings["Tfs"].ConnectionString));
		}

		private static Project MapProject(TfsTeamProject project)
		{
			return new Project
				{
					Name = project.Name
				};
		}

		private static Identity MapIdentity(TeamFoundationIdentity identity)
		{
			return new Identity
			{
				DisplayName = identity.DisplayName,
				Id = identity.UniqueUserId,
				IsActive = identity.IsActive,
				UniqueName = identity.UniqueName
			};
		} 
	}
}
