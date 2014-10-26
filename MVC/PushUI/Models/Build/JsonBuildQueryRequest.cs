
namespace PushUI.Models.Build
{
	public class JsonBuildQueryRequest : IJsonRequest
	{
		public string apiPath { get; set; }

		public JsonBuildQueryRequest() { }
		public JsonBuildQueryRequest(IViewModel model)
		{
			apiPath = model.ApiPath;
		}
	}
}