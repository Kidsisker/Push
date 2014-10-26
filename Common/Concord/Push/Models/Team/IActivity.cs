
namespace Concord.Push.Models.Team
{
	public interface IActivity
	{
		string Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		IActivityType Type { get; set; }
	}
}
