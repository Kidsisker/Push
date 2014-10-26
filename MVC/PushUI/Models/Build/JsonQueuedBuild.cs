using System;
using Concord.Push.Models.Build;

namespace PushUI.Models.Build
{
	public class JsonQueuedBuild
	{
		public JsonBuildDetail build { get; set; }
		public string definition { get; set; }
		public int id { get; set; }
		public int position { get; set; }
		public DateTime queueTime { get; set; }
		public string requestedBy { get; set; }
		public string projectName { get; set; }

		public JsonQueuedBuild() { }
		public JsonQueuedBuild(IQueuedBuild model)
		{
			build = model.Build != null ? new JsonBuildDetail(model.Build) : null;
			definition = model.BuildDefinition != null ? model.BuildDefinition.Name : null;
			id = model.Id;
			position = model.QueuePosition;
			queueTime = model.QueueTime;
			requestedBy = model.RequestedBy;
			projectName = model.TeamProject;
		}
	}
}
