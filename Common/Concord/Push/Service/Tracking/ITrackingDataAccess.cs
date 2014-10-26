using System.Collections.Generic;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;

namespace Concord.Push.Service.Tracking
{
	/// <summary>
	/// service detailing all functions that can be applied on Team data
	/// </summary>
	public interface ITrackingDataAccess
	{
		WorkItem GetWorkItem(int workItemId);

		IEnumerable<WorkItem> GetWorkItemsByState(Project project, string state);

		IEnumerable<MergeTask> GetWorkItemsWithMigrationScripts(Project project, string state);

		MergeTask CreateMergeTask(IWorkItem parent, string taskTitle);

		IEnumerable<MergeTask> CreateMergeTasks(Project project, string parentState, string taskTitle);
	}
}
