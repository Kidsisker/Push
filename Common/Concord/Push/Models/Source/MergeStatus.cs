
namespace Concord.Push.Models.Source
{
	public class MergeStatus : IPullStatus
	{
		public bool HaveResolvableWarnings { get; set; }
		public bool NoActionNeeded { get; set; }
		public long NumBytes { get; set; }
		public int NumConflicts { get; set; }
		public int NumFailures { get; set; }
		public long NumFiles { get; set; }
		public int NumOperations { get; set; }
		public int NumResolvedConflicts { get; set; }
		public int NumUpdated { get; set; }
		public int NumWarnings { get; set; }
		public string Message { get; set; }
	}
}
