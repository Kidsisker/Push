using System.Collections.Generic;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;
using  Concord.Push.Service.Source;

namespace Concord.Push.Business.Source
{
	public class Source
	{
		/// <summary>
		/// get changesets using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="workItem"></param>
		public static IEnumerable<Changeset> GetChangesets(ISourceDataAccess dataAccess, IWorkItem workItem)
		{
			var da = new Data.Source.SourceData(dataAccess);
			return da.GetChangesets(workItem);
		}

		/// <summary>
		/// get changesets using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="workItem"></param>
		/// <param name="environment"></param>
		public static IEnumerable<Changeset> GetChangesets(ISourceDataAccess dataAccess, IWorkItem workItem, MergeEnvironment environment)
		{
			var da = new Data.Source.SourceData(dataAccess);
			return da.GetChangesets(workItem, environment);
		}

		/// <summary>
		/// get changesets using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="project"></param>
		/// <param name="state"></param>
		/// <param name="environment"></param>
		public static IEnumerable<Changeset> GetChangesetsByWorkItemState(ISourceDataAccess dataAccess, Project project, string state, MergeEnvironment environment)
		{
			var da = new Data.Source.SourceData(dataAccess);
			return da.GetChangesetsByWorkItemState(project, state, environment);
		}

		/// <summary>
		/// get merge candidates for a project and environment
		/// </summary>
		/// <param name="dataAccess"></param>
		/// <param name="project"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		public static IEnumerable<Changeset> GetMergeCandidates(ISourceDataAccess dataAccess, Project project, MergeEnvironment environment)
		{
			var da = new Data.Source.SourceData(dataAccess);
			return da.GetMergeCandidates(project, environment);
		}

		/// <summary>
		/// merge changeset using provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="changeset"></param>
		/// <param name="environment"></param>
		public static MergeStatus MergeChangeset(ISourceDataAccess dataAccess, Changeset changeset, MergeEnvironment environment)
		{
			var da = new Data.Source.SourceData(dataAccess);
			return da.MergeChangeset(changeset, environment);
		}

		/// <summary>
		/// commit pending changes using the provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="comment"></param>
		/// <param name="associatedWorkItems"></param>
		public static Changeset CommitPending(ISourceDataAccess dataAccess, string comment, IEnumerable<WorkItem> associatedWorkItems)
		{
			var da = new Data.Source.SourceData(dataAccess);
			return da.CommitPending(comment, associatedWorkItems);
		}

		/// <summary>
		/// commit pending changes for a changeset that have been merged for the specified target using the provided data access instance
		/// </summary>
		/// <param name="dataAccess">the implementation of the data access to use when getting the data</param>
		/// <param name="environment"></param>
		/// <param name="comment"></param>
		/// <param name="changeset"></param>
		public static Changeset CommitChangeset(ISourceDataAccess dataAccess, Changeset changeset, MergeEnvironment environment, string comment)
		{
			var da = new Data.Source.SourceData(dataAccess);
			return da.CommitChangeset(changeset, environment, comment);
		}
	}
}
