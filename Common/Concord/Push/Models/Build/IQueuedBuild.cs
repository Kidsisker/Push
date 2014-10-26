using System;
using System.Collections.ObjectModel;

namespace Concord.Push.Models.Build
{
	// Summary:
	//     The interface for a queued build on the server that is running Team Foundation
	//     Build.
	public interface IQueuedBuild
	{
		// Summary:
		//     The batch ID of the queued build.
		Guid BatchId { get; set; }
		//
		// Summary:
		//     Gets the build in progress associated with this queued build.
		//
		// Returns:
		//     The build in progress associated with this queued build if the queued build
		//     is in progress. Null if the queued build is still in queue.
		IBuildDetail Build { get; set; }
		//
		// Summary:
		//     Gets the build controller on which this queued build will be built.
		//
		// Returns:
		//     The build controller on which this queued build will be built.
		//IBuildController BuildController { get; set; }
		//
		// Summary:
		//     Gets the URI of the build controller on which this queued build will be built.
		//
		// Returns:
		//     The URI of the build controller on which this queued build will be built.
		Uri BuildControllerUri { get; set; }
		//
		// Summary:
		//     Gets the build definition for which this queued build will be built.
		//
		// Returns:
		//     The build definition for which this queued build will be built.
		IBuildDefinition BuildDefinition { get; set; }
		//
		// Summary:
		//     Gets the URI of the build definition for which this queued build will be
		//     built.
		//
		// Returns:
		//     The URI of the build definition for which this queued build will be built.
		Uri BuildDefinitionUri { get; set; }
		//
		// Summary:
		//     Gets the collection of builds for this queue entry.
		ReadOnlyCollection<IBuildDetail> Builds { get; set; }
		//
		// Summary:
		//     Gets the server that owns this queued build.
		//
		// Returns:
		//     The server that owns this queued build.
		//IBuildServer BuildServer { get; set; }
		//
		// Summary:
		//     Gets the time for which sources should be retrieved for the queued build.
		//
		// Returns:
		//     The time for which sources should be retrieved for the queued build.
		string CustomGetVersion { get; set; }
		//
		// Summary:
		//     Gets the location where to drop the outputs of the queued build.
		//
		// Returns:
		//     The location where to drop the outputs of the queued build.
		string DropLocation { get; set; }
		//
		// Summary:
		//     Gets the time for which sources should be retrieved for the queued build.
		//
		// Returns:
		//     The time for which sources should be retrieved for the queued build.
		//GetOption GetOption { get; set; }
		//
		// Summary:
		//     Gets the ID of this queued build.
		//
		// Returns:
		//     The ID of this queued build.
		int Id { get; set; }
		//
		// Summary:
		//     Gets or sets the priority of this queued build.
		//
		// Returns:
		//     The priority of this queued build.
		//QueuePriority Priority { get; set; set; }
		//
		// Summary:
		//     Gets the process parameters that were used for this build.
		//
		// Returns:
		//     The process parameters that were used for this build.
		string ProcessParameters { get; set; }
		//
		// Summary:
		//     Gets the current position of the build in the queue.
		//
		// Returns:
		//     The current position of the build in the queue.
		int QueuePosition { get; set; }
		//
		// Summary:
		//     Gets the time when the build was queued.
		//
		// Returns:
		//     The time when the build was queued.
		DateTime QueueTime { get; set; }
		//
		// Summary:
		//     Gets the reason that the build was queued.
		//
		// Returns:
		//     The reason that the build was queued.
		//BuildReason Reason { get; set; }
		//
		// Summary:
		//     Gets the user who requested the queued build.
		//
		// Returns:
		//     The user who requested the queued build.
		string RequestedBy { get; set; }
		//
		// Summary:
		//     Gets the display name of the user who requested the build.
		string RequestedByDisplayName { get; set; }
		//
		// Summary:
		//     Gets the user for whom the queued build was requested.
		//
		// Returns:
		//     The user for whom the queued build was requested.
		string RequestedFor { get; set; }
		//
		// Summary:
		//     Gets the display name of the user for whom the build was requested.
		string RequestedForDisplayName { get; set; }
		//
		// Summary:
		//     Gets the shelveset that will be built.
		//
		// Returns:
		//     The shelveset that will be built.
		string ShelvesetName { get; set; }
		//
		// Summary:
		//     Gets the status of the queued build.
		//
		// Returns:
		//     The status of the queued build.
		//QueueStatus Status { get; set; }
		//
		// Summary:
		//     Gets the team project that owns this queued build.
		//
		// Returns:
		//     The team project that owns this queued build.
		string TeamProject { get; set; }
	}
}
