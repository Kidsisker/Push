using System;
using System.Collections.Generic;
using Concord.Push.Models.Team;
using Concord.Push.Models.Tracking;

namespace Concord.Push.Models.Source
{
	public class Changeset
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; }
		public Identity CommittedBy { get; set; }
		public Branch Branch { get; set; }
		public IEnumerable<WorkItem> WorkItems { get; set; }

	}

	


	
}
