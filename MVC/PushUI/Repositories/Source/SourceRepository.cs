using System.Linq;
using System.Collections.Generic;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;
using Concord.Push.Service.Source;
using PushUI.Models.Source;
using PushUI.Models.Team;
using PushUI.Models.Tracking;
using BLL = Concord.Push.Business.Source;

namespace PushUI.Repositories.Source
{
	/// <summary>
	/// repository class to perform functions related to source control.
	/// </summary>
	public class SourceRepository : ISourceRepository
	{
		public ISourceDataAccess SourceDataAccess { get; set; }

		/// <summary>
		/// default constructor (passes in dataaccess layer dependency)
		/// </summary>
		/// <param name="sourceDataAccess"></param>
		public SourceRepository(ISourceDataAccess sourceDataAccess)
		{
			SourceDataAccess = sourceDataAccess;
		}

		/// <summary>
		/// gets changesets for specified work item
		/// </summary>
		/// <param name="model"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		public IEnumerable<JsonMergeCandidateChangeset> GetJsonChangesets(JsonWorkItem model, JsonMergeEnvironment environment)
		{
			return BLL.Source.GetChangesets(SourceDataAccess, (WorkItem)model, (MergeEnvironment)environment).Select(c => new JsonMergeCandidateChangeset(c, environment));
		}

		/// <summary>
		/// gets changesets for all work items of a specified state
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public IEnumerable<JsonMergeCandidateChangeset> GetJsonChangesetsByWorkItemState(JsonMergeViewModel model)
		{
			var result = BLL.Source.GetChangesetsByWorkItemState(SourceDataAccess, (Project)model.mergeMethodOption.project, model.mergeMethodOption.methodValue, (MergeEnvironment)model.environment);
			return result != null ? result.Select(c => new JsonMergeCandidateChangeset(c, model)) : null;
		}

		public IEnumerable<JsonMergeCandidateChangeset> GetJsonMergeCandidates(JsonProject project, JsonMergeEnvironment environment)
		{
			var result = BLL.Source.GetMergeCandidates(SourceDataAccess, (Project) project, (MergeEnvironment) environment);
			return result != null ? result.Select(c => new JsonMergeCandidateChangeset(c, environment)) : null;
		}

		/// <summary>
		/// merges changeset for the specified environment
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public JsonMergeStatus MergeJsonChangeset(JsonMergeCandidateChangeset model)
		{
			var result = BLL.Source.MergeChangeset(SourceDataAccess, (Changeset)model, (MergeEnvironment)model.environment);
			return result != null ? new JsonMergeStatus(result) : null;
		}

		/// <summary>
		/// commits pending changes for the local workspace
		/// </summary>
		/// <param name="comment"></param>
		/// <param name="associatedWorkItems"></param>
		/// <returns></returns>
		public JsonChangeset JsonCommitPending(string comment, IEnumerable<JsonWorkItem> associatedWorkItems)
		{
			var result = BLL.Source.CommitPending(SourceDataAccess, comment, associatedWorkItems.Select(w => (WorkItem) w));
			return result != null ? new JsonChangeset(result) : null;
		}

		/// <summary>
		/// commits pending changes for a changeset that have been merged for the specified target and maps to the desired view model
		/// </summary>
		/// <param name="changeset"></param>
		/// <param name="comment"></param>
		public JsonChangeset JsonCommitChangeset(JsonMergeCandidateChangeset changeset, string comment)
		{
			return JsonCommitChangeset(changeset, changeset.environment, comment);
		}

		/// <summary>
		/// commits pending changes for a changeset that have been merged for the specified target and maps to the desired view model
		/// </summary>
		/// <param name="environment"></param>
		/// <param name="comment"></param>
		/// <param name="changeset"></param>
		public JsonChangeset JsonCommitChangeset(IJsonChangeset changeset, JsonMergeEnvironment environment, string comment)
		{
			var result = BLL.Source.CommitChangeset(SourceDataAccess, (Changeset)new JsonChangeset(changeset), (MergeEnvironment)environment, comment);
			return result != null ? new JsonChangeset(result) : null;
		}
	}
}