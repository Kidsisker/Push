using System.Collections.Generic;
using System.Linq;
using Concord.Push.Models.Source;
using Concord.Push.Models.Tracking;
using PushUI.Models.Tracking;

namespace PushUI.Models.Source
{
	public class JsonMergeCandidateChangeset : IJsonChangeset
	{
		public int id { get; set; }
		public string committedBy { get; set; }
		public IEnumerable<JsonWorkItem> workItems { get; set; }
		public string branch { get; set; }
		public JsonMergeEnvironment environment { get; set; }
		public bool autoCommit { get; set; }
		public JsonMergeStatus status { get; set; }
		public JsonChangeset mergedChangeset { get; set; }

		public JsonMergeCandidateChangeset() { }
		public JsonMergeCandidateChangeset(Changeset model)
		{
			id = model.Id;
			committedBy = model.CommittedBy != null ? (model.CommittedBy.DisplayName ?? model.CommittedBy.UniqueName) : null;
			workItems = model.WorkItems.ToList().Select(m => new JsonWorkItem(m));
			branch = model.Branch != null ? model.Branch.Name : null;
		}
		public JsonMergeCandidateChangeset(Changeset model, JsonMergeEnvironment mergeEnvironment) : this(model)
		{
			environment = mergeEnvironment;
		}
		public JsonMergeCandidateChangeset(Changeset model, JsonMergeViewModel viewModel) : this(model, viewModel.environment)
		{
			autoCommit = viewModel.autoCommit;
		}
		public JsonMergeCandidateChangeset(IJsonChangeset model)
		{
			CastInterface(model);
		}
		public JsonMergeCandidateChangeset(JsonMergeCandidateChangeset model)
		{
			Cast(model);
		}
		public static explicit operator Changeset(JsonMergeCandidateChangeset json)
		{
			return new Changeset
			{
				Id = json.id,
				WorkItems = json.workItems != null ? json.workItems.Select(w => (WorkItem)w) : null,
				Branch = !string.IsNullOrEmpty(json.branch) ? new Branch { Name = json.branch } : null
			};
		}

		private void CastInterface(IJsonChangeset model)
		{
			id = model.id;
			committedBy = model.committedBy;
			workItems = model.workItems;
			branch = model.branch;
		}

		private void Cast(JsonMergeCandidateChangeset model)
		{
			CastInterface(model);
			environment = model.environment;
			autoCommit = model.autoCommit;
			status = model.status;
			mergedChangeset = model.mergedChangeset;
		}
	}
}