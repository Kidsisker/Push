
using PushUI.Models.Team;

namespace PushUI.Models.Tracking
{
	public class JsonWorkItemRequest : IJsonRequest, IJsonWorkItemRequest
	{
		public string apiPath { get; set; }
		public JsonProject project { get; set; }
		public WorkItemQueryMethod method { get; set; }
		public string methodValue { get; set; }

		public JsonWorkItemRequest() { }
		public JsonWorkItemRequest(IViewModel model)
		{
			apiPath = model.ApiPath;
		}
	}
}