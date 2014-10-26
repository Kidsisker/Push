
namespace PushUI.Models.Build
{
	public class BuildManagerViewModel : IViewModel
	{
		public string ApiPath { get; set; }
		public BuildQueryViewModel BuildQueryViewModel { get; set; }
	}
}