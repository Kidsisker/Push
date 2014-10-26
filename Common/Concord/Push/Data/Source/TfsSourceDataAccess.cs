using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Configuration;
using Concord.Push.Data.Build;
using Concord.Push.Data.Team;
using Concord.Push.Data.Tracking;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;
using Concord.Push.Service.Source;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using BuildStatus = Concord.Push.Models.Build.BuildStatus;
using Project = Concord.Push.Models.Team.Project;
using Changeset = Concord.Push.Models.Source.Changeset;
using TfsChangeset = Microsoft.TeamFoundation.VersionControl.Client.Changeset;
using WorkItem = Concord.Push.Models.Tracking.WorkItem;
using TfsWorkItem = Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem;

namespace Concord.Push.Data.Source
{
	/// <summary>
	/// data access class to get source data from the TFS API
	/// </summary>
	public class TfsSourceDataAccess : ISourceDataAccess
	{
		/// <summary>
		/// gets changeset for the specified work item
		/// </summary>
		/// <param name="workItem"></param>
		public IEnumerable<Changeset> GetChangesets(IWorkItem workItem)
		{
			return new List<Changeset>(); // todo
		}

		/// <summary>
		/// gets changesets for the specified merge environment
		/// </summary>
		/// <param name="workItem"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		public IEnumerable<Changeset> GetChangesets(IWorkItem workItem, MergeEnvironment environment)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var tfsWorkItem = TfsTrackingDataAccess.GetTfsWorkItem(workItem.Id);
			return GetTfsChangesetsBySource(tfs, tfsWorkItem, environment).ToList();
		}

		/// <summary>
		/// gets changesets for all work items of the specified state
		/// </summary>
		/// <param name="project"></param>
		/// <param name="state"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		public IEnumerable<Changeset> GetChangesetsByWorkItemState(Project project, string state, MergeEnvironment environment)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var workItems = TfsTrackingDataAccess.GetTfsWorkItemsByState(tfs, project, state);
			return workItems.SelectMany(w => GetTfsChangesetsBySource(tfs, w, environment)).ToList();
		}

		/// <summary>
		/// get merge candidates for a project and environment
		/// </summary>
		/// <param name="project"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		public IEnumerable<Changeset> GetMergeCandidates(Project project, MergeEnvironment environment)
		{

			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var projects = TfsTeamDataAccess.GetTfsProjects().ToList();
			var versionControl = tfs.GetService<VersionControlServer>();

			var mergeCandidates = new List<Changeset>();
			projects.ForEach(tfsProject => environment.Relationships.ToList().ForEach(relationship =>
				{
					var projectName = !tfsProject.ServerItem.EndsWith("/") ? tfsProject.ServerItem + "/" : tfsProject.Name;
					var sourcePath = string.Format("{0}{1}/", projectName, relationship.Source.Name);
					var targetPath = string.Format("{0}{1}/", projectName, relationship.Target.Name);
					try
					{
						var candidates = versionControl.GetMergeCandidates(sourcePath, targetPath, RecursionType.Full).ToList();
						candidates.ForEach(candidate =>
							{
								var workItems =
									candidate.Changeset.WorkItems.Where(
										workItem => 
											workItem.State != "Active" 
											&& workItem.State != "In Review"
											&& workItem.Type != workItem.Project.WorkItemTypes["Task"] // ignore changesets associated with task. Will create another audit that checks for changesets ONLY associated to task.
											&& string.Compare(project.Name, workItem.Project.Name, StringComparison.OrdinalIgnoreCase) == 0);
								if (workItems.Any())
									mergeCandidates.Add(MapChangeset(candidate.Changeset, relationship.Source));
							});
					}
					catch (Exception e)
					{
					}
				}));

			return mergeCandidates;
		}

		/// <summary>
		/// merges a changeset for the specified environment
		/// </summary>
		/// <param name="changeset"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		public MergeStatus MergeChangeset(Changeset changeset, MergeEnvironment environment)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var versionControl = tfs.GetService<VersionControlServer>();
			var workspace = versionControl.GetWorkspace(Workstation.Current.Name, tfs.AuthorizedIdentity.UniqueName);
			var tfsChangeset = versionControl.GetChangeset(changeset.Id);
			var statusList = new List<MergeStatus>();
			tfsChangeset.Changes.ToList().ForEach(change =>
				{
					var sourcePath = change.Item.ServerItem.Substring(0, change.Item.ServerItem.LastIndexOf("/", StringComparison.Ordinal));
					sourcePath = !sourcePath.EndsWith("/") ? sourcePath + "/" : sourcePath;
					var mergeRelationship = GetMergeRelationship(changeset, environment);
					if (mergeRelationship == null)
					{
						statusList.Add(new MergeStatus
						{
							NumFailures = 1,
							Message = string.Format("There are no merge relationships configured for {0}", changeset.Branch.Name)
						});
						return;
					}
					var targetPath = sourcePath.ToLower().Replace(string.Format("/{0}/", changeset.Branch.Name.ToLower()), string.Format("/{0}/", mergeRelationship.Target.Name));

					if (string.Compare(sourcePath, targetPath, StringComparison.OrdinalIgnoreCase) == 0) // this happens with feature branches. They will get picked up on next "changeset".
						return;
					
					// Get Latest first
					string[] itemsSpec = {targetPath};
					workspace.Get(itemsSpec, VersionSpec.Latest, RecursionType.Full, GetOptions.Overwrite);

					// Get ready to Merge.
					MergeStatus status;
					var verSpec = new ChangesetVersionSpec(tfsChangeset.ChangesetId);
					try
					{
						status = MapMergeStatus(workspace.Merge(sourcePath, targetPath, verSpec, verSpec, LockLevel.None, RecursionType.Full, MergeOptions.None));
					}
					catch (Exception e)
					{
						status = new MergeStatus
						{
							NumFailures = 1,
							Message = e.Message
						};
					}
					var conflicts = workspace.QueryConflicts(new[] {targetPath}, true);
					conflicts.ToList().ForEach(conflict =>
						{
							workspace.MergeContent(conflict, false);
							if (conflict.ContentMergeSummary.TotalConflicting > 0) return;
							// Conflict was resolved. Does not require a human!
							conflict.Resolution = Resolution.AcceptMerge;
							// If the conflict is successfully resolved, the IsResolved property is set to true. 
							// If resolving this conflict caused other conflicts to be deleted besides the current conflict, the list of other deleted conflicts appears in resolvedConflicts.
							Conflict[] resolvedConflicts;
							workspace.ResolveConflict(conflict, out resolvedConflicts);
							if (!conflict.IsResolved) return;

							var totalResolved = resolvedConflicts.Count();
							status.NumResolvedConflicts = status.NumResolvedConflicts + 1 + totalResolved;
							status.NumConflicts = status.NumConflicts - 1 - totalResolved;
						});
					statusList.Add(status);
				});
			return new MergeStatus // create summary of all merges for this changeset
				{
					HaveResolvableWarnings = statusList.Any(s => s.HaveResolvableWarnings),
					NoActionNeeded = statusList.All(s => s.NoActionNeeded),
					NumBytes = statusList.Sum(s => s.NumBytes),
					NumConflicts = statusList.Sum(s => s.NumConflicts),
					NumFailures = statusList.Sum(s => s.NumFailures),
					NumFiles = statusList.Sum(s => s.NumFiles),
					NumOperations = statusList.Sum(s => s.NumOperations),
					NumResolvedConflicts = statusList.Sum(s => s.NumResolvedConflicts),
					NumUpdated = statusList.Sum(s => s.NumUpdated),
					NumWarnings = statusList.Sum(s => s.NumWarnings)
				};
		}

		/// <summary>
		/// commits pending changes for a changeset that have been merged to the specified target and associates specified work items and comment
		/// </summary>
		/// <param name="changeset"></param>
		/// <param name="environment"></param>
		/// <param name="comment"></param>
		/// <returns></returns>
		public Changeset CommitChangeset(Changeset changeset, MergeEnvironment environment, string comment)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var versionControl = tfs.GetService<VersionControlServer>();
			var workspace = versionControl.GetWorkspace(Workstation.Current.Name, tfs.AuthorizedIdentity.UniqueName);
			var tfsChangeset = versionControl.GetChangeset(changeset.Id);
			var changes = (from change in tfsChangeset.Changes select change).ToList();
			var relationship = GetMergeRelationship(changeset, environment);
			var pending = workspace.GetPendingChanges(changes
				.Select(m => new ItemSpec(m.Item.ServerItem.ToLower()
					.Replace(string.Format("/{0}/", changeset.Branch.Name.ToLower()), string.Format("/{0}/", relationship.Target.Name)), RecursionType.None)).ToArray(), false, int.MaxValue, null, true).ToList();
			var pendingChangesetChanges = pending.ToArray();
				//.Where(p => p.IsMerge && p.MergeSources.Any(source => changes.Any(change => string.Compare(change.Item.ServerItem, source.ServerItem, StringComparison.OrdinalIgnoreCase) == 0))).ToArray();

			return Commit(workspace, pendingChangesetChanges, comment, changeset.WorkItems);
		}

		/// <summary>
		/// commits pending changes for the local workspace and associates specified work items and comment
		/// </summary>
		/// <param name="comment"></param>
		/// <param name="associatedWorkItems"></param>
		/// <returns></returns>
		public Changeset CommitPending(string comment, IEnumerable<WorkItem> associatedWorkItems)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var versionControl = tfs.GetService<VersionControlServer>();
			var workspace = versionControl.GetWorkspace(Workstation.Current.Name, tfs.AuthorizedIdentity.UniqueName);
			return Commit(workspace, workspace.GetPendingChanges(), comment, associatedWorkItems);
		}

		/// <summary>
		/// determines merge relationship for a branch and target environment
		/// </summary>
		/// <param name="branch"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		private static MergeRelationship GetMergeRelationship(Branch branch, MergeEnvironment environment)
		{
			return environment.Relationships.ToList().Find(r => string.Compare(r.Source.Name, branch.Name, StringComparison.OrdinalIgnoreCase) == 0);
		}

		/// <summary>
		/// determines merge relationship for a changeset and target environment
		/// </summary>
		/// <param name="changeset"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		private static MergeRelationship GetMergeRelationship(Changeset changeset, MergeEnvironment environment)
		{
			return GetMergeRelationship(changeset.Branch, environment);
		}

		/// <summary>
		/// commits changes for the local workspace and associates specified work items and comment
		/// </summary>
		/// <param name="workspace"></param>
		/// <param name="changes"></param>
		/// <param name="comment"></param>
		/// <param name="associatedWorkItems"></param>
		/// <returns></returns>
		private static Changeset Commit(Workspace workspace, PendingChange[] changes, string comment, IEnumerable<WorkItem> associatedWorkItems)
		{
			if (changes == null || !changes.Any())
				return null;
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var versionControl = tfs.GetService<VersionControlServer>();

			var associateTfsWorkItems = associatedWorkItems.Select(w => TfsTrackingDataAccess.GetTfsWorkItem(w.Id))
				.GroupBy(g => g.Id)
				.Select(w => new WorkItemCheckinInfo(w.First(), WorkItemCheckinAction.Associate)).ToArray();
			comment = string.Format("{0}: {1} {2}", string.Join(",", associateTfsWorkItems.OrderBy(o => o.WorkItem.Id).Select(a => a.WorkItem.Id.ToString(CultureInfo.InvariantCulture))), comment, ConfigurationManager.AppSettings["AutomationMessage"]);
			var changesetId = 0;
			try
			{
				changesetId = workspace.CheckIn(changes, comment, null, associateTfsWorkItems, null);
			}
			catch (GatedCheckinException gatedException)
			{
				// Gated check-in is required. Get the list of build definitions affected by the check-in
				var buildDefs = gatedException.AffectedBuildDefinitions;

				// Take first build def. I don't really have any better way to determine it.
				var buildEnum = buildDefs.GetEnumerator();
				buildEnum.MoveNext();
				var buildDef = buildEnum.Current;
				var gatedBuildDefUri = buildDef.Value;
				var shelvesetSpecName = gatedException.ShelvesetName;
				var shelvesetTokens = shelvesetSpecName.Split(new[] { ';' });

				// Create a build request for the gated check-in build
				var buildServer = tfs.GetService<IBuildServer>();
				var buildRequest = buildServer.CreateBuildRequest(gatedBuildDefUri);
				buildRequest.ShelvesetName = shelvesetTokens[0]; // Specify the name of the existing shelveset
				buildRequest.Reason = BuildReason.CheckInShelveset; // Check-in the shelveset if successful 
				buildRequest.GatedCheckInTicket = gatedException.CheckInTicket; // Associate the check-in

				var build = TfsBuildDataAccess.QueueTfsBuild(buildRequest, null, true);
				if (build.Build != null)
				{
					var firstOrDefault = build.Build.Changesets.FirstOrDefault(); // shouldn't be null for gated check in, for pete's sake.
					if (firstOrDefault != null) changesetId = firstOrDefault.Id;
					if (build.Build.Status == BuildStatus.Succeeded)
						workspace.Undo(changes);
				}
			}

			var newTfsChangeset = versionControl.GetChangeset(changesetId);
			return newTfsChangeset == null ? null : MapChangeset(newTfsChangeset);
		}

		/// <summary>
		/// gets changesets for a particular path (branch) and workitem
		/// </summary>
		/// <param name="tfs"></param>
		/// <param name="workItem"></param>
		/// <param name="environment"></param>
		private static IEnumerable<Changeset> GetTfsChangesetsBySource(TfsTeamProjectCollection tfs, TfsWorkItem workItem, MergeEnvironment environment)
		{
			return environment.Relationships
				.SelectMany(relationship => GetTfsChangesetsByPathName(tfs, workItem, relationship.Source.Name)
					.Select(c => MapChangeset(c, relationship.Source))).ToList();

		}

		/// <summary>
		/// gets ts changesets for a particular path (branch) and workitem
		/// </summary>
		/// <param name="tfs"></param>
		/// <param name="workItem"></param>
		/// <param name="pathName"></param>
		internal static IEnumerable<TfsChangeset> GetTfsChangesetsByPathName(TfsTeamProjectCollection tfs, TfsWorkItem workItem, string pathName)
		{
			var versionControl = tfs.GetService<VersionControlServer>();

			return (from extLink in workItem.Links.OfType<ExternalLink>()
					let artifact = LinkingUtilities.DecodeUri(extLink.LinkedArtifactUri)
					where
						String.Equals(artifact.ArtifactType, "Changeset", StringComparison.Ordinal)
					select
						versionControl.ArtifactProvider.GetChangeset(new Uri(extLink.LinkedArtifactUri)))
						.Where(w => w.Changes.Any(a => a.Item.ServerItem.IndexOf(string.Format("/{0}/", pathName), StringComparison.OrdinalIgnoreCase) > 0)).ToList();

		}

		private static Changeset MapChangeset(TfsChangeset dataModel, Branch branch)
		{
			var changeset = MapChangeset(dataModel);
			changeset.Branch = branch;
			return changeset;
		}

		private static Changeset MapChangeset(TfsChangeset dataModel)
		{
			// todo: implement mapper layer
			return new Changeset
				{
					Id = dataModel.ChangesetId,
					CreationDate = dataModel.CreationDate,
					CommittedBy = new Identity
						{
							UniqueName = dataModel.Committer ?? dataModel.Owner,
							DisplayName = dataModel.CommitterDisplayName ?? dataModel.OwnerDisplayName
						},
					WorkItems = dataModel.WorkItems != null ? dataModel.WorkItems.OrderBy(o => o.Id).Select(TfsTrackingDataAccess.MapWorkItem) : null
				};
		}

		private static MergeStatus MapMergeStatus(GetStatus dataModel)
		{
			// todo: implement mapper layer
			return new MergeStatus
			{
				HaveResolvableWarnings = dataModel.HaveResolvableWarnings,
				NoActionNeeded = dataModel.NoActionNeeded,
				NumBytes = dataModel.NumBytes,
				NumConflicts = dataModel.NumConflicts,
				NumFailures = dataModel.NumFailures,
				NumFiles = dataModel.NumFiles,
				NumOperations = dataModel.NumOperations,
				NumResolvedConflicts = dataModel.NumResolvedConflicts,
				NumUpdated = dataModel.NumUpdated,
				NumWarnings = dataModel.NumWarnings
			};
		}
	}
}
