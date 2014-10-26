using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Concord.Push.Models.Source;

namespace Concord.Push.Models.Build
{
	public class BuildDetail : IBuildDetail
	{
		public Uri BuildControllerUri { get; set; }
		public IBuildDefinition BuildDefinition { get; set; }
		public Uri BuildDefinitionUri { get; set; }
		public bool BuildFinished { get; set; }
		public string BuildNumber { get; set; }
		public string DropLocation { get; set; }
		public string DropLocationRoot { get; set; }
		public DateTime FinishTime { get; set; }
		public bool IsDeleted { get; set; }
		public bool KeepForever { get; set; }
		public string LabelName { get; set; }
		public string LastChangedBy { get; set; }
		public string LastChangedByDisplayName { get; set; }
		public DateTime LastChangedOn { get; set; }
		public string LogLocation { get; set; }
		public string ProcessParameters { get; set; }
		public string Quality { get; set; }
		public string RequestedBy { get; set; }
		public string RequestedFor { get; set; }
		public ReadOnlyCollection<int> RequestIds { get; set; }
		public string ShelvesetName { get; set; }
		public string SourceGetVersion { get; set; }
		public DateTime StartTime { get; set; }
		public BuildStatus Status { get; set; }
		public string TeamProject { get; set; }
		public Uri Uri { get; set; }
		public IEnumerable<Changeset> Changesets { get; set; }
	}
}
