
namespace Concord.Push.Models.Tracking
{
	public class MergeTask : ITask
	{
		public int Id { get; set; }
		public string State { get; set; }
		public string Title { get; set; }
		public WorkItemType Type { get; set; }
		public IWorkItem Parent { get; set; }

		public MergeTask() { }
		public MergeTask(IWorkItem model)
		{
			Id = model.Id;
			State = model.State;
			Title = model.Title;
		}
	}
}
