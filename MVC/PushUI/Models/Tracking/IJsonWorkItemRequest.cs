using PushUI.Models.Team;

namespace PushUI.Models.Tracking
{
	public interface IJsonWorkItemRequest
	{
		JsonProject project { get; set; }
		WorkItemQueryMethod method { get; set; }
		string methodValue { get; set; }
	}
}