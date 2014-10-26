
namespace Concord.Push.Models.Team
{
	public class Identity : IIdentity
	{
		public int Id { get; set; }
		public string DisplayName { get; set; }
		public bool IsActive { get; set; }
		public string UniqueName { get; set; }
	}
}
