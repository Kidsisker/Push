using System.Collections.Generic;
using Concord.Push.Models.Build;
using Concord.Push.Service.Build;

namespace Concord.Push.Business.Build
{
	public class Build
	{
		/// <summary>
		/// query builds. query options set for now.
		/// </summary>
		/// <param name="dataAccess"></param>
		/// <returns></returns>
		public static IEnumerable<IBuildDetail> QueryBuilds(IBuildDataAccess dataAccess)
		{
			var DAL = new Data.Build.BuildData(dataAccess);
			return DAL.QueryBuilds();
		}

		/// <summary>
		/// query queued builds. query options set for now.
		/// </summary>
		/// <param name="dataAccess"></param>
		/// <returns></returns>
		public static IEnumerable<IQueuedBuild> QueryQueuedBuilds(IBuildDataAccess dataAccess)
		{
			var DAL = new Data.Build.BuildData(dataAccess);
			return DAL.QueryQueuedBuilds();
		}

		/// <summary>
		/// queue a build with the provided build definition
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="project"></param>
		/// <param name="buildDefUri"></param>
		/// <param name="quality"></param>
		/// <param name="wait"></param>
		public static IQueuedBuild QueueBuild(IBuildDataAccess dataAccess, string project, string buildDefUri, string quality, bool wait)
		{
			var DAL = new Data.Build.BuildData(dataAccess);
			return DAL.QueueBuild(project, buildDefUri, quality, wait);
		}

		/// <summary>
		/// queue a build with the provided build request
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="buildRequest"></param>
		public static IQueuedBuild QueueBuild(IBuildDataAccess dataAccess, IBuildRequest buildRequest)
		{
			var DAL = new Data.Build.BuildData(dataAccess);
			return DAL.QueueBuild(buildRequest);
		}
	}
}
