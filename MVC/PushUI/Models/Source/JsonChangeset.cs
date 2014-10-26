using System.Collections.Generic;
using System.Linq;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;
using PushUI.Models.Tracking;

namespace PushUI.Models.Source
{
	public class JsonChangeset : IJsonChangeset
	{
		public int id { get; set; }
		public string committedBy { get; set; }
		public IEnumerable<JsonWorkItem> workItems { get; set; }
		public string branch { get; set; }

		public JsonChangeset() { }
		public JsonChangeset(Changeset model)
		{
			id = model.Id;
			committedBy = model.CommittedBy != null ? (model.CommittedBy.DisplayName ?? model.CommittedBy.UniqueName) : null;
			workItems = model.WorkItems.ToList().Select(m => new JsonWorkItem(m));
			branch = model.Branch != null ? model.Branch.Name : null;
		}
		public JsonChangeset(IJsonChangeset model)
		{
			id = model.id;
			workItems = model.workItems;
			branch = model.branch;
		}
		public static explicit operator Changeset(JsonChangeset json)
		{
			return new Changeset
				{
					Id = json.id,
					CommittedBy = new Identity
						{
							DisplayName = json.committedBy
						},
					WorkItems = json.workItems != null ? json.workItems.Select(w => (WorkItem)w) : null,
					Branch = !string.IsNullOrEmpty(json.branch) ? new Branch{ Name = json.branch } : null
				};
		}
	}
}