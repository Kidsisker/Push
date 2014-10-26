using System.Collections.Generic;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;
using  Concord.Push.Service.Source;

namespace Concord.Push.Data.Source
{
	/// <summary>
	/// our typical "Data" class 
	/// </summary>
	public class SourceData
	{
		readonly ISourceDataAccess _sourceDataAccess;

		/// <summary>
		/// default constructor (with dependency input)
		/// </summary>
		/// <param name="sourceDataAccess">the data access object to use when getting data</param>
		public SourceData(ISourceDataAccess sourceDataAccess)
		{
			_sourceDataAccess = sourceDataAccess;
		}

		/// <summary>
		/// get the list of changesets
		/// </summary>
		/// <param name="workItem"></param>
		public IEnumerable<Changeset> GetChangesets(IWorkItem workItem)
		{
			return _sourceDataAccess.GetChangesets(workItem);
		}

		/// <summary>
		/// get the list of changesets
		/// </summary>
		/// <param name="workItem"></param>
		/// <param name="environment"></param>
		public IEnumerable<Changeset> GetChangesets(IWorkItem workItem, MergeEnvironment environment)
		{
			return _sourceDataAccess.GetChangesets(workItem, environment);
		}

		/// <summary>
		/// get the list of changesets
		/// </summary>
		/// <param name="project"></param>
		/// <param name="state"></param>
		/// <param name="environment"></param>
		public IEnumerable<Changeset> GetChangesetsByWorkItemState(Project project, string state, MergeEnvironment environment)
		{
			return _sourceDataAccess.GetChangesetsByWorkItemState(project, state, environment);
		}

		/// <summary>
		/// get the list of merge candidates for a project and environment
		/// </summary>
		/// <param name="project"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		public IEnumerable<Changeset> GetMergeCandidates(Project project, MergeEnvironment environment)
		{
			return _sourceDataAccess.GetMergeCandidates(project, environment);
		}

		/// <summary>
		/// merge changeset
		/// </summary>
		/// <param name="changeset"></param>
		/// <param name="environment"></param>
		public MergeStatus MergeChangeset(Changeset changeset, MergeEnvironment environment)
		{
			return _sourceDataAccess.MergeChangeset(changeset, environment);
		}

		/// <summary>
		/// commit pending changes
		/// </summary>
		/// <param name="comment"></param>
		/// <param name="associatedWorkItems"></param>
		public Changeset CommitPending(string comment, IEnumerable<WorkItem> associatedWorkItems)
		{
			return _sourceDataAccess.CommitPending(comment, associatedWorkItems);
		}

		/// <summary>
		/// commit pending changes for a changeset that have been merged for the specified target
		/// </summary>
		/// <param name="changeset"></param>
		/// <param name="environment"></param>
		/// <param name="comment"></param>
		public Changeset CommitChangeset(Changeset changeset, MergeEnvironment environment, string comment)
		{
			return _sourceDataAccess.CommitChangeset(changeset, environment, comment);
		}
	}
}
