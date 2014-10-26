using System.Collections.Generic;
using Concord.Push.Service.Source;
using PushUI.Models.Source;
using PushUI.Models.Team;
using PushUI.Models.Tracking;

namespace PushUI.Repositories.Source
{
	/// <summary>
	/// interface for Source Control level repository functionality
	/// </summary>
	public interface ISourceRepository
	{
		ISourceDataAccess SourceDataAccess { get; set; }

		/// <summary>
		/// gets changesets for a specific environment and maps to the desired view model
		/// </summary>
		/// <param name="model"></param>
		/// <param name="environment"></param>
		IEnumerable<JsonMergeCandidateChangeset> GetJsonChangesets(JsonWorkItem model, JsonMergeEnvironment environment);

		/// <summary>
		/// gets changesets for all work items of a specific state and maps to the desired view model
		/// </summary>
		/// <param name="model"></param>
		IEnumerable<JsonMergeCandidateChangeset> GetJsonChangesetsByWorkItemState(JsonMergeViewModel model);

		/// <summary>
		/// gets merge candidates for a project and environment
		/// </summary>
		/// <param name="project"></param>
		/// <param name="environment"></param>
		/// <returns></returns>
		IEnumerable<JsonMergeCandidateChangeset> GetJsonMergeCandidates(JsonProject project, JsonMergeEnvironment environment);

		/// <summary>
		/// gets changesets for a specific environment and maps to the desired view model
		/// </summary>
		/// <param name="model"></param>
		JsonMergeStatus MergeJsonChangeset(JsonMergeCandidateChangeset model);

		/// <summary>
		/// commits pending changes and returns new changeset
		/// </summary>
		/// <param name="comment"></param>
		/// <param name="associatedWorkItems"></param>
		JsonChangeset JsonCommitPending(string comment, IEnumerable<JsonWorkItem> associatedWorkItems);

		/// <summary>
		/// commits pending changes for a changeset that have been merged for the specified target and maps to the desired view model
		/// </summary>
		/// <param name="comment"></param>
		/// <param name="changeset"></param>
		JsonChangeset JsonCommitChangeset(JsonMergeCandidateChangeset changeset, string comment);

		/// <summary>
		/// commits pending changes for a changeset that have been merged for the specified target and maps to the desired view model
		/// </summary>
		/// <param name="environment"></param>
		/// <param name="comment"></param>
		/// <param name="changeset"></param>
		JsonChangeset JsonCommitChangeset(IJsonChangeset changeset, JsonMergeEnvironment environment, string comment);
	}
}