using System.Collections.Generic;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;

namespace Concord.Push.Service.Source
{
	/// <summary>
	/// service detailing all functions that can be applied on source control data
	/// </summary>
	public interface ISourceDataAccess
	{
		IEnumerable<Changeset> GetChangesets(IWorkItem workItem);

		IEnumerable<Changeset> GetChangesets(IWorkItem workItem, MergeEnvironment environment);

		IEnumerable<Changeset> GetChangesetsByWorkItemState(Project project, string state, MergeEnvironment environment);

		IEnumerable<Changeset> GetMergeCandidates(Project project, MergeEnvironment environment);

		MergeStatus MergeChangeset(Changeset changeset, MergeEnvironment environment);

		Changeset CommitChangeset(Changeset changeset, MergeEnvironment environment, string comment);

		Changeset CommitPending(string comment, IEnumerable<WorkItem> associatedWorkItems);
	}
}
