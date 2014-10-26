using System;

namespace Concord.Push.Models.Team
{
	public interface ITeamProjectCollection : IServiceProvider
	{
		void Dispose();
	}
}
