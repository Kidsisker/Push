
namespace Concord.Push.Models.Team
{
	public interface IProject
	{
		int Id { get; set; }
		bool IsDefault { get; set; }
		string Name { get; set; }
	}
}
