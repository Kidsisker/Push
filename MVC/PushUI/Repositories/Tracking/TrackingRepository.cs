using System.Collections.Generic;
using System.Linq;
using Concord.Push.Models.Team;
using Concord.Push.Service.Tracking;
using PushUI.Models.Source;
using PushUI.Models.Team;
using PushUI.Models.Tracking;
using BLL = Concord.Push.Business.Tracking;

namespace PushUI.Repositories.Tracking
{
	/// <summary>
	/// repository class to perform functions related to tracking (work items, etc.) data.
	/// </summary>
	public class TrackingRepository : ITrackingRepository
	{
		public ITrackingDataAccess TrackingDataAccess { get; set; }

		/// <summary>
		/// default constructor (passes in dataaccess layer dependency)
		/// </summary>
		public TrackingRepository(ITrackingDataAccess trackingDataAccess)
		{
			TrackingDataAccess = trackingDataAccess;
		}

		public IEnumerable<JsonMergeMethod> GetJsonMergeMethods(JsonProject project, JsonMergeEnvironment environment)
		{
			return new List<JsonMergeMethod>
				{
					new JsonMergeMethod
						{
							method = WorkItemQueryMethod.State,
							name = "State",
							selected = true,
							options = environment.states.Select(state => new JsonMergeMethodOption
								{
									project = project,
									method = WorkItemQueryMethod.State,
									methodValue = state,
									selected = true
								})
						},
					new JsonMergeMethod
						{
							method = WorkItemQueryMethod.SavedQuery,
							name = "Saved Query"
						},
					new JsonMergeMethod
						{
							method = WorkItemQueryMethod.DynamicQuery,
							name = "Dynamic Query"
						}
				};
		}

		public IEnumerable<JsonWorkItem> GetJsonWorkItems(JsonWorkItemRequest request)
		{
			return request.method != WorkItemQueryMethod.State ? null : BLL.Tracking.GetWorkItemsByState(TrackingDataAccess, (Project)request.project, request.methodValue).Select(w => new JsonWorkItem(w)); //todo: allow for other WorkItemQueryMethod
		}

		public IEnumerable<JsonWorkItem> GetWorkItemsWithMigrationScripts(JsonWorkItemRequest request)
		{
			return request.method != WorkItemQueryMethod.State ? null : BLL.Tracking.GetWorkItemsWithMigrationScripts(TrackingDataAccess, (Project)request.project, request.methodValue).Select(w => new JsonWorkItem(w)); //todo: allow for other WorkItemQueryMethod
		}

		public IEnumerable<JsonWorkItem> CreateMergeTasks(JsonCreateWorkItemRequest request)
		{
			return request.method != WorkItemQueryMethod.State ? null : BLL.Tracking.CreateMergeTasks(TrackingDataAccess, (Project)request.project, request.methodValue, request.title).Select(w => new JsonWorkItem(w)); //todo: allow for other WorkItemQueryMethod
		}
	}
}