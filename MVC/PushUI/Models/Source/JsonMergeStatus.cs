
using Concord.Push.Models.Source;

namespace PushUI.Models.Source
{
	public class JsonMergeStatus
	{
		public bool haveResolvableWarnings { get; set; }
		public bool noActionNeeded { get; set; }
		public long numBytes { get; set; }
		public int numConflicts { get; set; }
		public int numFailures { get; set; }
		public long numFiles { get; set; }
		public int numOperations { get; set; }
		public int numResolvedConflicts { get; set; }
		public int numUpdated { get; set; }
		public int numWarnings { get; set; }
		public string message { get; set; }

		public JsonMergeStatus() { }
		public JsonMergeStatus(IPullStatus model)
		{
			haveResolvableWarnings = model.HaveResolvableWarnings;
			noActionNeeded = model.NoActionNeeded;
			numBytes = model.NumBytes;
			numConflicts = model.NumConflicts;
			numFailures = model.NumFailures;
			numFiles = model.NumFiles;
			numOperations = model.NumOperations;
			numResolvedConflicts = model.NumResolvedConflicts;
			numUpdated = model.NumUpdated;
			numWarnings = model.NumWarnings;
			message = model.Message;
		}
	}
}
