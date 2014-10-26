
namespace Concord.Push.Models.Team
{
	public interface IIdentity
	{
		string DisplayName { get; set; }
		int Id { get; set; }
		bool IsActive { get; set; }
		string UniqueName { get; set; }
	}
}
