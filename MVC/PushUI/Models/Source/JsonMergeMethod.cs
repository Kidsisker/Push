using System.Collections.Generic;
using PushUI.Models.Team;
using PushUI.Models.Tracking;

namespace PushUI.Models.Source
{
	public class JsonMergeMethod
	{
		public WorkItemQueryMethod method { get; set; }
		public string name { get; set; }
		public bool selected { get; set; }
		public IEnumerable<JsonMergeMethodOption> options { get; set; }
	}

	public class JsonMergeMethodOption : IJsonWorkItemRequest
	{
		public JsonProject project { get; set; }
		public WorkItemQueryMethod method { get; set; }
		public string methodValue { get; set; }
		public bool selected { get; set; }
	}
}