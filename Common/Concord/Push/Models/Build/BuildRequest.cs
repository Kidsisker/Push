using System;

namespace Concord.Push.Models.Build
{
	public class BuildRequest : IBuildRequest
	{
		public Guid BatchId { get; set; }
		public Uri BuildControllerUri { get; set; }
		public IBuildDefinition BuildDefinition { get; set; }
		public Uri BuildDefinitionUri { get; set; }
		public string CustomGetVersion { get; set; }
		public string DropLocation { get; set; }
		public string GatedCheckInTicket { get; set; }
		public int MaxQueuePosition { get; set; }
		public bool Postponed { get; set; }
		public string ProcessParameters { get; set; }
		public string Project { get; set; }
		public string Quality { get; set; }
		public string RequestedFor { get; set; }
		public string ShelvesetName { get; set; }
		public bool Wait { get; set; }
	}
}
