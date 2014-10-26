
namespace Concord.Push.Models.Team
{
	public class Project : IProject
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsDefault { get; set; }
	}
}
