using System;
using System.Linq;
using System.Collections.Generic;
using Concord.Push.Models.Build;
using PushUI.Models.Source;

namespace PushUI.Models.Build
{
	public class JsonBuildDetail
	{
		public string number { get; set; }
		public string definition { get; set; }
		public bool isFinished { get; set; }
		public DateTime finishTime { get; set; }
		public string quality { get; set; }
		public string requestedBy { get; set; }
		public DateTime startTime { get; set; }
		public string projectName { get; set; }
		public IEnumerable<JsonChangeset> changesets { get; set; }

		public JsonBuildDetail() { }
		public JsonBuildDetail(IBuildDetail model)
		{
			number = model.BuildNumber;
			definition = model.BuildDefinition != null ? model.BuildDefinition.Name : null;
			isFinished = model.BuildFinished;
			finishTime = model.FinishTime;
			quality = model.Quality;
			requestedBy = model.RequestedBy;
			startTime = model.StartTime;
			projectName = model.TeamProject;
			changesets = model.Changesets != null ? model.Changesets.Select(s => new JsonChangeset(s)) : null;
		}
	}
}
