
namespace Concord.Push.Models.Tracking
{
	public class WorkItem : IWorkItem
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public WorkItemType Type { get; set; }
		public string State { get; set; }
	}
}
