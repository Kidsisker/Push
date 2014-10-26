using System;
using System.Collections.Generic;
using Concord.Push.Models.Team;
using Concord.Push.Service.Team;

namespace Concord.Push.Data.Team
{
	/// <summary>
	/// our typical "Data" class 
	/// </summary>
	public class TeamData
	{
		readonly ITeamDataAccess _teamDataAccess;

		/// <summary>
		/// default constructor (with dependency input)
		/// </summary>
		/// <param name="teamDataAccess">the data access object to use when getting data</param>
		public TeamData(ITeamDataAccess teamDataAccess)
		{
			_teamDataAccess = teamDataAccess;
		}

		/// <summary>
		/// get team projects
		/// </summary>
		public IEnumerable<Project> GetProjects()
		{
			return _teamDataAccess.GetProjects();
		}

		/// <summary>
		/// get the team project service
		/// </summary>
		public IServiceProvider GetTeamProjectCollection()
		{
			return _teamDataAccess.GetTeamProjectCollection();
		}

	}
}
