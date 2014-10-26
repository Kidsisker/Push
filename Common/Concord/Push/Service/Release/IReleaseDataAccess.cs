using System.Collections.Generic;
using Concord.Push.Models.Source;
using Concord.Push.Models.Tracking;

namespace Concord.Push.Service.Release
{
	/// <summary>
	/// service detailing all functions that can be applied on release data
	/// </summary>
	public interface IReleaseDataAccess
	{
		IEnumerable<Changeset> GetChangesets(IWorkItem workItem);
	}
}
