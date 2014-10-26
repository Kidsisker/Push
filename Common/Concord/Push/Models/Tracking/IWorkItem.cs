
namespace Concord.Push.Models.Tracking
{
	public interface IWorkItem
	{
		int Id { get; set; }
		string State { get; set; }
		string Title { get; set; }
		WorkItemType Type { get; set; }
	}
}
