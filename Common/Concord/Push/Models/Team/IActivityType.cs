
using System.Collections.Generic;

namespace Concord.Push.Models.Team
{
	public interface IActivityType
	{
		string Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		ActivityCategory Category { get; set; }
		IEnumerable<string> Tags { get; set; }
	}
}
