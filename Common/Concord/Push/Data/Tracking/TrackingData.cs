using System.Collections.Generic;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;
using Concord.Push.Service.Tracking;

namespace Concord.Push.Data.Tracking
{
	/// <summary>
	/// our typical "Data" class 
	/// </summary>
	public class TrackingData
	{
		readonly ITrackingDataAccess _trackingDataAccess;

		/// <summary>
		/// default constructor (with dependency input)
		/// </summary>
		/// <param name="trackingDataAccess">the data access object to use when getting data</param>
		public TrackingData(ITrackingDataAccess trackingDataAccess)
		{
			_trackingDataAccess = trackingDataAccess;
		
		}

		public WorkItem GetWorkItem(int workItemId)
		{
			return _trackingDataAccess.GetWorkItem(workItemId);
		}

		public IEnumerable<WorkItem> GetWorkItemsByState(Project project, string state)
		{
			return _trackingDataAccess.GetWorkItemsByState(project, state);
		}

		public IEnumerable<MergeTask> GetWorkItemsWithMigrationScripts(Project project, string state)
		{
			return _trackingDataAccess.GetWorkItemsWithMigrationScripts(project, state);
		}

		public MergeTask CreateMergeTask(WorkItem parent, string taskTitle)
		{
			return _trackingDataAccess.CreateMergeTask(parent, taskTitle);
		}

		public IEnumerable<MergeTask> CreateMergeTasks(Project project, string parentState, string taskTitle)
		{
			return _trackingDataAccess.CreateMergeTasks(project, parentState, taskTitle);
		}
	}
}
