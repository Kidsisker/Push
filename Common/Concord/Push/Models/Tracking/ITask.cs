
namespace Concord.Push.Models.Tracking
{
	public interface ITask : IWorkItem
	{
		IWorkItem Parent { get; set; }
	}
}
