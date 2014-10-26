using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;
using PushUI.Models.Team;

namespace PushUI.Models.Tracking
{
	public class JsonWorkItemType
	{
		public string name { get; set; }
		public string description { get; set; }
		public JsonProject project { get; set; }

		public JsonWorkItemType(){ }
		public JsonWorkItemType(WorkItemType model)
		{
			name = model.Name;
			description = model.Description;
			project = model.Project != null ? new JsonProject(model.Project) : null;
		}

		public static explicit operator WorkItemType(JsonWorkItemType json)
		{
			return new WorkItemType
			{
				Name = json.name,
				Description = json.description,
				Project = json.project != null ? (Project)json.project : null
			};
		}
	}
}
