using System;
using System.Collections.Generic;
using Concord.Push.Models.Team;

namespace Concord.Push.Service.Team
{
	/// <summary>
	/// service detailing all functions that can be applied on Team data
	/// </summary>
	public interface ITeamDataAccess
	{
		IServiceProvider GetTeamProjectCollection();

		IEnumerable<Project> GetProjects();
	}
}
