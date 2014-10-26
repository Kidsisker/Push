
namespace Concord.Push.Models.Source
{
	public interface IPullStatus
	{
		bool HaveResolvableWarnings { get; set; }
		bool NoActionNeeded { get; set; }
		long NumBytes { get; set; }
		int NumConflicts { get; set; }
		int NumFailures { get; set; }
		long NumFiles { get; set; }
		int NumOperations { get; set; }
		int NumResolvedConflicts { get; set; }
		int NumUpdated { get; set; }
		int NumWarnings { get; set; }
		string Message { get; set; }
	}
}
