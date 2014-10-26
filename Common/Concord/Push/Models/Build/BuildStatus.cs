using System;

namespace Concord.Push.Models.Build
{
	// Summary:
	//     This enumeration represents the status of builds and build steps.
	[Flags]
	public enum BuildStatus
	{
		// Summary:
		//     No status available.
		None = 0,
		//
		// Summary:
		//     Build is in progress.
		InProgress = 1,
		//
		// Summary:
		//     Build succeeded.
		Succeeded = 2,
		//
		// Summary:
		//     Build is partially succeeded.
		PartiallySucceeded = 4,
		//
		// Summary:
		//     Build failed.
		Failed = 8,
		//
		// Summary:
		//     Build is stopped.
		Stopped = 16,
		//
		// Summary:
		//     Build is not started.
		NotStarted = 32,
		//
		// Summary:
		//     All status applies.
		All = 63,
	}
}
