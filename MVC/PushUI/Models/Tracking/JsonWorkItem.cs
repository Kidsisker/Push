using Concord.Push.Models.Tracking;

namespace PushUI.Models.Tracking
{
	public class JsonWorkItem
	{
		public int id { get; set; }
		public string title { get; set; }
		public string state { get; set; }
		public JsonWorkItemType type { get; set; }

		public JsonWorkItem() { }
		public JsonWorkItem(IWorkItem model)
		{
			id = model.Id;
			title = model.Title;
			state = model.State;
			type = model.Type != null ? new JsonWorkItemType(model.Type) : null;
		}

		public static explicit operator WorkItem(JsonWorkItem json)
		{
			return new WorkItem
				{
					Id = json.id,
					Title = json.title,
					State = json.state,
					Type = json.type != null ? (WorkItemType)json.type : null
				};
		}
	}
}