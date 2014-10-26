using System.Collections.Generic;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;
using Concord.Push.Service.Tracking;

namespace Concord.Push.Business.Tracking
{
	public class Tracking
	{
		/// <summary>
		/// get work items using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="workItem"></param>
		public static WorkItem GetWorkItem(ITrackingDataAccess dataAccess, IWorkItem workItem)
		{
			var DAL = new Data.Tracking.TrackingData(dataAccess);
			return DAL.GetWorkItem(workItem.Id);
		}

		/// <summary>
		/// get work items where associated changesets have migration scripts using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="project"></param>
		/// <param name="state"></param>
		public static IEnumerable<MergeTask> GetWorkItemsWithMigrationScripts(ITrackingDataAccess dataAccess, Project project, string state)
		{
			var DAL = new Data.Tracking.TrackingData(dataAccess);
			return DAL.GetWorkItemsWithMigrationScripts(project, state);
		}

		/// <summary>
		/// get work items using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="project"></param>
		/// <param name="state"></param>
		public static IEnumerable<WorkItem> GetWorkItemsByState(ITrackingDataAccess dataAccess, Project project, string state)
		{
			var DAL = new Data.Tracking.TrackingData(dataAccess);
			return DAL.GetWorkItemsByState(project, state);
		}

		/// <summary>
		/// create a merge task for the supplied parent work item, using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="parent"></param>
		/// <param name="taskTitle"></param>
		public static MergeTask CreateMergeTask(ITrackingDataAccess dataAccess, WorkItem parent, string taskTitle)
		{
			var DAL = new Data.Tracking.TrackingData(dataAccess);
			return DAL.CreateMergeTask(parent, taskTitle);
		}

		/// <summary>
		/// create merge tasks for all work items of a state, using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="project"></param>
		/// <param name="parentState"></param>
		/// <param name="taskTitle"></param>
		public static IEnumerable<MergeTask> CreateMergeTasks(ITrackingDataAccess dataAccess, Project project, string parentState, string taskTitle)
		{
			var DAL = new Data.Tracking.TrackingData(dataAccess);
			return DAL.CreateMergeTasks(project, parentState, taskTitle);
		}
	}
}
