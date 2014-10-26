using System.Collections.Generic;

namespace Concord.Push.Models.Team
{
	public enum ActivityCategory
	{
		Unknown = 0,
		Team = 1,
		Merge = 2,
		Tracking = 3,
		Build = 4,
		Release = 5
	}

	public class ActivityType : IActivityType
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ActivityCategory Category { get; set; }
		public IEnumerable<string> Tags { get; set; } 
	}
}
