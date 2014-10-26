using Concord.Push.Models.Team;

namespace PushUI.Models.Team
{
	public class ActivityViewModel : IViewModel
	{
		public IProject Project { get; set; }
		public string ApiPath { get; set; }
	}
}