
namespace Concord.Push.Models.Team
{
	public class Activity : IActivity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IActivityType Type { get; set; } 
	}
}
