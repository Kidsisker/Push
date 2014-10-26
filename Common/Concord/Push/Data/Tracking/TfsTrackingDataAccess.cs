using System.Collections.Generic;
using System.Linq;
using Concord.Push.Data.Source;
using Concord.Push.Data.Team;
using Concord.Push.Models.Tracking;
using Concord.Push.Service.Tracking;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Project = Concord.Push.Models.Team.Project;
using TfsWorkItem = Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem;
using WorkItem = Concord.Push.Models.Tracking.WorkItem;
using WorkItemType = Concord.Push.Models.Tracking.WorkItemType;

namespace Concord.Push.Data.Tracking
{
	/// <summary>
	/// data access class to get tracking data from the Tfs API 
	/// </summary>
	public class TfsTrackingDataAccess : ITrackingDataAccess
	{
		public WorkItem GetWorkItem(int workItemId)
		{
			return MapWorkItem(GetTfsWorkItem(workItemId));
		}

		public IEnumerable<WorkItem> GetWorkItemsByState(Project project, string state)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			return GetTfsWorkItemsByState(tfs, project, state).Select(MapWorkItem);
		}

		public IEnumerable<MergeTask> GetWorkItemsWithMigrationScripts(Project project, string state)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();

			var tasks = new List<MergeTask>();
			var workItems = GetWorkItemsByState(project, state);
			workItems.ToList().ForEach(workItem =>
				{
					var tfsWorkItem = GetTfsWorkItem(workItem.Id);
					// Check for data script and tag.
					var changesets = TfsSourceDataAccess.GetTfsChangesetsByPathName(tfs, tfsWorkItem, "Data Scripts").ToList(); // todo: not hard code. obvs.
					if (changesets.Any())
					{
						tasks.AddRange(changesets.SelectMany(s => s.Changes).GroupBy(g => g.Item.ServerItem).Select(change => new MergeTask
						{
							Title = string.Format("Execute Data Script: {0};", change.First().Item.ServerItem),
							Parent = workItem
						}));
					}
				});
			return tasks;
		}

		public MergeTask CreateMergeTask(IWorkItem parent, string taskTitle)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var store = tfs.GetService<WorkItemStore>();
			var project = store.Projects[parent.Type.Project.Name];
			var taskType = project.WorkItemTypes["Task"];
			var hierarchyLinkType = store.WorkItemLinkTypes[CoreLinkTypeReferenceNames.Hierarchy];

			var tfsParent = GetTfsWorkItem(parent.Id);
			if (tfsParent == null)
				return null;

			var existing = (from WorkItemLink l in tfsParent.WorkItemLinks select l).ToList()
					.Find(p => p.LinkTypeEnd.Name == "Child" && GetTfsWorkItem(p.TargetId).Title == taskTitle);
			if (existing != null)
			{
				var tfsExistingWorkItem = GetTfsWorkItem(existing.TargetId);
				return new MergeTask(MapWorkItem(tfsExistingWorkItem))
					{
						Parent = MapWorkItem(tfsParent)
					};
			}
			var task = new TfsWorkItem(taskType)
				{
					Title = taskTitle
				};

			task.Fields["Assigned To"].Value = "Jill LaMay"; // todo: get identity
			task.Links.Add(new WorkItemLink(hierarchyLinkType.ReverseEnd, parent.Id));
			
			// Check for data script and tag.
			var changesets = TfsSourceDataAccess.GetTfsChangesetsByPathName(tfs, tfsParent, "Data Scripts").ToList(); // todo: not hard code. obvs.
			if (changesets.Any())
			{
				changesets.SelectMany(s => s.Changes).GroupBy(g => g.Item.ServerItem).ToList().ForEach(f =>
					{
						var descr = string.Format("{0} Data Script: {1};", task.Fields["Description"].Value, f.First().Item.ServerItem);
						task.Title = descr; // temp
					});
				//task.Fields["Tags"].Value = "data-script"; // todo: Not in 2012 :(
			}
			// todo: turn off save while testing
			//task.Save();

			//task.State = "Active";
			//task.Save();

			return new MergeTask(MapWorkItem(task))
				{
					Parent = parent
				};
		}

		public IEnumerable<MergeTask> CreateMergeTasks(Project project, string parentState, string taskTitle)
		{
			var workItems = GetWorkItemsByState(project, parentState);
			return workItems.Select(s => CreateMergeTask(s, taskTitle)).ToList();
		}

		internal static TfsWorkItem GetTfsWorkItem(int workItemId)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var store = tfs.GetService<WorkItemStore>();
			return store.GetWorkItem(workItemId);
		}

		internal static IEnumerable<TfsWorkItem> GetTfsWorkItemsByState(TfsTeamProjectCollection tfs, Project project, string state)
		{
			var store = tfs.GetService<WorkItemStore>();
			var workItems = store.Query(
			   "select [System.Id] " +
			   "from WorkItems " +
			   "where [System.TeamProject] = '" + project.Name + "' " + 
			   "and [System.State] = '" + state + "'");

			return (from TfsWorkItem w in workItems select w).ToList();
		}

		internal static WorkItem MapWorkItem(TfsWorkItem dataModel)
		{
			// todo: implement mapper
			return new WorkItem
				{
					Id = dataModel.Id,
					Title = dataModel.Title,
					Type = dataModel.Type != null ? new WorkItemType
						{
							Name = dataModel.Type.Name,
							Description = dataModel.Type.Description,
							Project = dataModel.Type.Project != null ? new Project
								{
									Id = dataModel.Type.Project.Id,
									Name = dataModel.Type.Project.Name
								} : null
						} : null,
					State = dataModel.State
				};
		}
	}
}
