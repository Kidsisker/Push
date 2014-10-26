
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using PushUI.Models.Team;

namespace PushUI.Models.Source
{
	public class JsonMergeRequest : IJsonRequest
	{
		public JsonProject project { get; set; }
		public JsonMergeEnvironment environment { get; set; }
		public string apiPath { get; set; }

		public JsonMergeRequest() { }
		public JsonMergeRequest(MergeViewModel model)
		{
			project = new JsonProject(model.Project);
			environment = new JsonMergeEnvironment(model.Environment);
			apiPath = model.ApiPath;
		}
		public static explicit operator MergeViewModel(JsonMergeRequest json)
		{
			return json != null ? new MergeViewModel
				{
					Project = (Project)json.project,
					Environment = (MergeEnvironment)json.environment,
					ApiPath = json.apiPath
				} : null;
		}
	}
}