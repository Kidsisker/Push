using PushUI.Models.Team;

namespace PushUI.Models.Tracking
{
	public class JsonCreateWorkItemRequest : IJsonRequest, IJsonWorkItemRequest
	{
		public string apiPath { get; set; }
		public JsonProject project { get; set; }
		public WorkItemQueryMethod method { get; set; }
		public string methodValue { get; set; }
		public string title { get; set; }

		public JsonCreateWorkItemRequest() { }
		public JsonCreateWorkItemRequest(IViewModel model)
		{
			apiPath = model.ApiPath;
		}
	}
}