using System.Collections.Generic;
using Concord.Push.Models.Build;
using Concord.Push.Models.Source;
using Concord.Push.Service.Build;
using PushUI.Models.Build;
using PushUI.Models.Source;

namespace PushUI.Repositories.Build
{
	/// <summary>
	/// interface for Build related functionality
	/// </summary>
	public interface IBuildRepository
	{
		IBuildDataAccess BuildDataAccess { get; set; }

		IEnumerable<JsonBuildDetail> QueryJsonBuilds();

		IEnumerable<JsonQueuedBuild> QueryJsonQueuedBuilds();

		IEnumerable<IQueuedBuild> QueueJsonMergeEnvironmentBuild(JsonMergeRequest model);

		IEnumerable<IQueuedBuild> QueueEnvironmentBuild(Environment model, bool wait);

		IQueuedBuild QueueBuild(string project, string buildDefUri, string quality, bool wait);
	}
}