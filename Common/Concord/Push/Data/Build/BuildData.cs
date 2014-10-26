using System.Collections.Generic;
using Concord.Push.Models.Build;
using Concord.Push.Service.Build;

namespace Concord.Push.Data.Build
{
	/// <summary>
	/// our typical "Data" class 
	/// </summary>
	public class BuildData
	{
		readonly IBuildDataAccess _buildDataAccess;

		/// <summary>
		/// default constructor (with dependency input)
		/// </summary>
		/// <param name="buidDataAccess">the data access object to use when getting data</param>
		public BuildData(IBuildDataAccess buidDataAccess)
		{
			_buildDataAccess = buidDataAccess;
		}

		public IEnumerable<IBuildDetail> QueryBuilds()
		{
			return _buildDataAccess.QueryBuilds();
		}

		public IEnumerable<IQueuedBuild> QueryQueuedBuilds()
		{
			return _buildDataAccess.QueryQueuedBuilds();
		}

		public IQueuedBuild QueueBuild(string project, string buildDefUri, string quality, bool wait)
		{
			return _buildDataAccess.QueueBuild(project, buildDefUri, quality, wait);
		}

		public IQueuedBuild QueueBuild(IBuildRequest buildRequest)
		{
			return _buildDataAccess.QueueBuild(buildRequest);
		}
	}
}
