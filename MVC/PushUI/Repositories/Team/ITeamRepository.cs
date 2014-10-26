using System;
using System.Collections.Generic;
using Concord.Push.Models.Team;
using Concord.Push.Service.Team;
using PushUI.Models.Team;

namespace PushUI.Repositories.Team
{
	/// <summary>
	/// interface for Source Control level repository functionality
	/// </summary>
	public interface ITeamRepository
	{
		ITeamDataAccess TeamDataAccess { get; set; }

		/// <summary>
		/// gets team connection
		/// </summary>
		IServiceProvider GetTeamProjectCollection();

		/// <summary>
		/// get the configure default project
		/// </summary>
		/// <returns></returns>
		TeamProject GetDefaultProject();

		/// <summary>
		/// gets team projects
		/// </summary>
		IEnumerable<TeamProject> GetProjects();

		/// <summary>
		/// gets a team project based on name
		/// </summary>
		TeamProject GetProject(string name);

		/// <summary>
		/// gets a team project based on name
		/// </summary>
		JsonTeamProject GetJsonProject(string name);

		/// <summary>
		/// gets team projects
		/// </summary>
		IEnumerable<JsonTeamProject> GetJsonProjects();
	}
}