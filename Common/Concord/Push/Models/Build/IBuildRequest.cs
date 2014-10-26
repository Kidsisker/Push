using System;
using Concord.Push.Models.Team;

namespace Concord.Push.Models.Build
{
	// Summary:
	//     Interface for a build request on the server that is running Team Foundation
	//     Build.
	public interface IBuildRequest
	{
		// Summary:
		//     The batch ID for the request.
		Guid BatchId { get; set; }
		//
		// Summary:
		//     Gets or sets the build controller that will start the requested build.
		//
		// Returns:
		//     The build controller that will start the requested build.
		//IBuildController BuildController { get; set; }
		//
		// Summary:
		//     Gets the uniform resource identifier (URI) of the build controller that will
		//     start the requested build.
		//
		// Returns:
		//     The URI of the build controller that will start the requested build.
		Uri BuildControllerUri { get; }
		//
		// Summary:
		//     Gets the build definition for which the requested build should be started.
		//     Provides default values for BuildController and DropLocation.
		//
		// Returns:
		//     The build definition for which the requested build should be started. Provides
		//     default values for BuildController and DropLocation.
		IBuildDefinition BuildDefinition { get; }
		//
		// Summary:
		//     Gets the URI of the build definition for which the requested build should
		//     be started.
		//
		// Returns:
		//     The URI of the build definition for which the requested build should be started.
		Uri BuildDefinitionUri { get; set;  }
		//
		// Summary:
		//     Gets the build server from which this build request was created.
		//
		// Returns:
		//     The build server from which this build request was created.
		//IBuildServer BuildServer { get; }
		//
		// Summary:
		//     Gets or sets the custom get versionSpec. Valid only when GetOption is set
		//     to 'Custom'.
		//
		// Returns:
		//     The custom get versionSpec. Valid only when GetOption is set to 'Custom'.
		string CustomGetVersion { get; set; }
		//
		// Summary:
		//     Gets or sets the location in which to drop the output of the requested build.
		//
		// Returns:
		//     The location in which to drop the output of the requested build.
		string DropLocation { get; set; }
		//
		// Summary:
		//     Gets or sets an optional ticket that is issued by the server for gated check-in
		//     submissions.
		//
		// Returns:
		//     An optional ticket that is issued by the server for gated check-in submissions.
		string GatedCheckInTicket { get; set; }
		//
		// Summary:
		//     Gets or sets the time for which sources should be retrieved for the requested
		//     build. Valid settings are 'LatestOnQueue', 'LatestOnBuild', or 'Custom'.
		//
		// Returns:
		//     The time for which sources should be retrieved for the requested build. Valid
		//     settings are 'LatestOnQueue', 'LatestOnBuild', or 'Custom'.
		//GetOption GetOption { get; set; }
		//
		// Summary:
		//     Gets or sets the maximum position in the queue for the requested build at
		//     queue time. If the build request falls below this position in the queue,
		//     an exception will be thrown.
		//
		// Returns:
		//     The maximum position in the queue for the requested build at queue time.
		int MaxQueuePosition { get; set; }
		//
		// Summary:
		//     Gets or sets a flag that describes whether the build request will be submitted
		//     together with a postponed status.
		//
		// Returns:
		//     True to set the build request together with a postponed status. False not
		//     to set a postponed status.
		bool Postponed { get; set; }
		//
		// Summary:
		//     Gets or sets the priority for the requested build.
		//
		// Returns:
		//     The priority for the requested build.
		//QueuePriority Priority { get; set; }
		//
		// Summary:
		//     Gets an XML formatted string representing all the process parameters for
		//     this build.
		//
		// Returns:
		//     An XML formatted string representing all the process parameters for this
		//     build.
		string ProcessParameters { get; set; }
		string Project { get; set; }
		//
		// Summary:
		//     Gets or sets the quality to apply this build.
		//
		// Returns:
		//     The quality to apply to this build.
		string Quality { get; set; }
		//
		// Summary:
		//     Gets or sets the reason for the build request.
		//
		// Returns:
		//     The reason for the build request.
		//BuildReason Reason { get; set; }
		//
		// Summary:
		//     Gets or sets the user for whom the build is being requested.
		//
		// Returns:
		//     The user for whom the build is being requested.
		string RequestedFor { get; set; }
		//
		// Summary:
		//     Gets or sets an optional shelveset to be built.
		//
		// Returns:
		//     An optional shelveset to be built.
		string ShelvesetName { get; set; }
		//
		// Summary:
		//     Get or sets true or false to wait for build to complete.
		//
		// Returns:
		//     An optional shelveset to be built.
		bool Wait { get; set; }
	}
}
