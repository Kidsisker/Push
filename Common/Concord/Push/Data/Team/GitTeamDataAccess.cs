using System;
using System.Collections.Generic;
using Concord.Push.Models.Team;
using Concord.Push.Service.Team;

namespace Concord.Push.Data.Team
{
	/// <summary>
	/// data access class to get source data from the Git API 
	/// </summary>
	public class GitTeamDataAccess : ITeamDataAccess
	{
		/// <summary>
		/// gets team connection
		/// </summary>
		public IServiceProvider GetTeamProjectCollection()
		{
			return TfsTeamDataAccess.GetTfsTeamProjectCollection(); // todo Switch to GIT!!!!!!!!!!!! :)
		}

		public IEnumerable<Project> GetProjects()
		{
			throw new NotImplementedException();
		}
	}
}
