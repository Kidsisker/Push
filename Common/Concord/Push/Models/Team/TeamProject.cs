using System.Collections.Generic;
using Concord.Push.Models.Source;

namespace Concord.Push.Models.Team
{
	public class TeamProject : IProject
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsDefault { get; set; }
		public IEnumerable<MergeEnvironment> MergeEnvironments { get; set; }
		public IEnumerable<Activity> Activities { get; set; }
	}
}
