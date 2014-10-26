using System;
using System.Collections.ObjectModel;

namespace Concord.Push.Models.Build
{
	public class QueuedBuild : IQueuedBuild
	{
		public Guid BatchId { get; set; }
		public IBuildDetail Build { get; set; }
		public Uri BuildControllerUri { get; set; }
		public IBuildDefinition BuildDefinition { get; set; }
		public Uri BuildDefinitionUri { get; set; }
		public ReadOnlyCollection<IBuildDetail> Builds { get; set; }
		public string CustomGetVersion { get; set; }
		public string DropLocation { get; set; }
		public int Id { get; set; }
		public string ProcessParameters { get; set; }
		public int QueuePosition { get; set; }
		public DateTime QueueTime { get; set; }
		public string RequestedBy { get; set; }
		public string RequestedByDisplayName { get; set; }
		public string RequestedFor { get; set; }
		public string RequestedForDisplayName { get; set; }
		public string ShelvesetName { get; set; }
		public string TeamProject { get; set; }
	}
}
