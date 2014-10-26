using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Concord.Push.Models.Source;

namespace Concord.Push.Models.Build
{
	public interface IBuildDetail
	{
		// Summary:
		//     Gets the build controller used to perform this build. May be null.
		//
		// Returns:
		//     The build controller used to perform this build. May be null.
		//IBuildController BuildController { get; }
		//
		// Summary:
		//     Gets the URI of the build controller used to perform this build.
		//
		// Returns:
		//     The URI of the build controller used to perform this build.
		Uri BuildControllerUri { get; set; }
		//
		// Summary:
		//     Gets the build definition that owns this build. May be null.
		//
		// Returns:
		//     The build definition that owns this build. May be null.
		IBuildDefinition BuildDefinition { get; set; }
		//
		// Summary:
		//     Gets the URI of the build definition used to perform this build.
		//
		// Returns:
		//     The URI of the build definition used to perform this build.
		Uri BuildDefinitionUri { get; set; }
		//
		// Summary:
		//     Gets a flag indicating whether the build has finished.
		//
		// Returns:
		//     True if the build has finished. False otherwise.
		bool BuildFinished { get; set; }
		//
		// Summary:
		//     Gets or sets the number for this build.
		//
		// Returns:
		//     The build number of this build.
		string BuildNumber { get; set; }
		//
		// Summary:
		//     Gets the server that owns this build.
		//
		// Returns:
		//     The server that owns this build.
		//IBuildServer BuildServer { get; }
		//
		// Summary:
		//     Gets or sets the status of the compilation phase of this build.
		//
		// Returns:
		//     The status of the compilation phase of this build.
		//BuildPhaseStatus CompilationStatus { get; set; }
		//
		// Summary:
		//     Gets or sets the location for the output of the build.
		//
		// Returns:
		//     The location for the output of the build.
		string DropLocation { get; set; }
		//
		// Summary:
		//     Gets the root drop location of the build.
		//
		// Returns:
		//     The root drop location of the build.
		string DropLocationRoot { get; set; }
		//
		// Summary:
		//     Gets the time that this build finished.
		//
		// Returns:
		//     The time that this build finished.
		DateTime FinishTime { get; set; }
		//
		// Summary:
		//     Gets the collection of information nodes for this build.
		//
		// Returns:
		//     The collection of information nodes for this build.
		//IBuildInformation Information { get; }
		//
		// Summary:
		//     Gets a flag describing whether the build has been deleted.
		//
		// Returns:
		//     True if the build was deleted. False if the build is not deleted.
		bool IsDeleted { get; set; }
		//
		// Summary:
		//     Gets or sets a flag describing whether the build participates in the retention
		//     policy of the build definition or to keep the build forever.
		//
		// Returns:
		//     True to keep the build forever. False to follow the retention policy specified
		//     in the build definition.
		bool KeepForever { get; set; }
		//
		// Summary:
		//     Gets or sets the name of the label created for the build.
		//
		// Returns:
		//     The name of the label created for the build.
		string LabelName { get; set; }
		//
		// Summary:
		//     Gets the last user to change this build.
		//
		// Returns:
		//     The last user to change this build.
		string LastChangedBy { get; }
		//
		// Summary:
		//     Gets the display name of the last user to change the build.
		string LastChangedByDisplayName { get; }
		//
		// Summary:
		//     Gets the date and time of the last change to this build.
		//
		// Returns:
		//     The date and time of the last change to this build.
		DateTime LastChangedOn { get; }
		//
		// Summary:
		//     Gets or sets the location of the log file for this build.
		//
		// Returns:
		//     The location of the log file for this build.
		string LogLocation { get; set; }
		//
		// Summary:
		//     Gets an XML formatted string representing all the process parameters for
		//     this build.
		//
		// Returns:
		//     An XML formatted string representing all the process parameters for this
		//     build.
		string ProcessParameters { get; }
		//
		// Summary:
		//     Gets or sets the quality of this build.
		//
		// Returns:
		//     The quality of this build.
		string Quality { get; set; }
		//
		// Summary:
		//     Gets the reason the build exists. For more information about the use of this
		//     property, see Specify Build Triggers and Reasons
		//
		// Returns:
		//     The reason the build exists.
		//BuildReason Reason { get; }
		//
		// Summary:
		//     Gets the user who requested this build.
		//
		// Returns:
		//     The user who requested this build.
		string RequestedBy { get; set; }
		//
		// Summary:
		//     Gets the user for whom this build was requested.
		//
		// Returns:
		//     The user for whom this build was requested.
		string RequestedFor { get; set; }
		//
		// Summary:
		//     The request Ids that started this build.
		ReadOnlyCollection<int> RequestIds { get; }
		//
		// Summary:
		//     The requests that started this build.
		//ReadOnlyCollection<IQueuedBuild> Requests { get; }
		//
		// Summary:
		//     Gets the shelveset that was built.
		//
		// Returns:
		//     The shelveset that was built.
		string ShelvesetName { get; }
		//
		// Summary:
		//     Gets or sets the version specification for which the sources were retrieved
		//     for this build.
		//
		// Returns:
		//     The version specification for which the sources were retrieved for this build.
		string SourceGetVersion { get; set; }
		//
		// Summary:
		//     Gets the time that this build actually started.
		//
		// Returns:
		//     The time that this build actually started.
		DateTime StartTime { get; }
		//
		// Summary:
		//     Gets or sets the overall status of this build.
		//
		// Returns:
		//     The overall status of this build.
		BuildStatus Status { get; set; }
		//
		// Summary:
		//     Gets the team project that owns this build.
		//
		// Returns:
		//     The team project that owns this build.
		string TeamProject { get; }
		//
		// Summary:
		//     Gets or sets the status of the test phase of this build.
		//
		// Returns:
		//     The status of the test phase of this build.
		//BuildPhaseStatus TestStatus { get; set; }
		//
		// Summary:
		//     Gets the URI of this build.
		//
		// Returns:
		//     The URI of this build.
		Uri Uri { get; }

		IEnumerable<Changeset> Changesets { get; set; }
	}
}
