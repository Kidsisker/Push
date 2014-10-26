using System;
using System.Collections.Generic;
using Concord.Push.Models.Team;
using Concord.Push.Service.Team;

namespace Concord.Push.Business.Team
{
	public class Team
	{
		/// <summary>
		/// get changesets using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		public static IServiceProvider GetTeamProjectCollection(ITeamDataAccess dataAccess)
		{
			var DAL = new Data.Team.TeamData(dataAccess);
			return DAL.GetTeamProjectCollection();
		}

		/// <summary>
		/// get team projects using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		public static IEnumerable<Project> GetProjects(ITeamDataAccess dataAccess)
		{
			return dataAccess.GetProjects();
		}
	}
}
