using Concord.Push.Models.Source;
using Concord.Push.Models.Team;

namespace PushUI.Models.Source
{
	public class MergeViewModel : IViewModel
	{
		public IProject Project { get; set; }
		public MergeEnvironment Environment { get; set; }
		public string ApiPath { get; set; }
	}
}