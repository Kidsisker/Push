
using System.Collections.Generic;

namespace Concord.Push.Models.Source
{
	public enum Environment
	{
		Unknown = 0,
		Dev = 1,
		QA = 2,
		Staging = 3,
		Production = 4,
		MarriottQAExt = 5
	}

	public class MergeEnvironment
	{
		public string Name { get; set; }
		public Environment Source { get; set; }
		public Environment Target { get; set; }
		public IEnumerable<string> AllowedStates { get; set; }
		public IEnumerable<MergeRelationship> Relationships { get; set; }
	}
}