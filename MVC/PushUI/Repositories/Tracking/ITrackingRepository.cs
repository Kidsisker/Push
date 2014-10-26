using System.Collections.Generic;
using Concord.Push.Service.Tracking;
using PushUI.Models.Source;
using PushUI.Models.Team;
using PushUI.Models.Tracking;

namespace PushUI.Repositories.Tracking
{
	/// <summary>
	/// interface for Tracking repository functionality
	/// </summary>
	public interface ITrackingRepository
	{
		ITrackingDataAccess TrackingDataAccess { get; set; }

		IEnumerable<JsonMergeMethod> GetJsonMergeMethods(JsonProject project, JsonMergeEnvironment environment);

		IEnumerable<JsonWorkItem> GetJsonWorkItems(JsonWorkItemRequest request);

		IEnumerable<JsonWorkItem> GetWorkItemsWithMigrationScripts(JsonWorkItemRequest request);

		IEnumerable<JsonWorkItem> CreateMergeTasks(JsonCreateWorkItemRequest request);
	}
}