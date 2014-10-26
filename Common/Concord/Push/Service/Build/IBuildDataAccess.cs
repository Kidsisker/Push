using System.Collections.Generic;
using Concord.Push.Models.Build;

namespace Concord.Push.Service.Build
{
	/// <summary>
	/// service detailing all functions that can be applied on build related functions
	/// </summary>
	public interface IBuildDataAccess
	{
		IEnumerable<IBuildDetail> QueryBuilds();

		IEnumerable<IQueuedBuild> QueryQueuedBuilds();

		IQueuedBuild QueueBuild(string project, string buildDefUri, string quality, bool wait);

		IQueuedBuild QueueBuild(IBuildRequest buildRequest);
	}
}
