using System;

namespace Concord.Push.Models.Team
{
	public class TeamProjectCollection : ITeamProjectCollection
	{

		public Project GetProject(string name)
		{
			return new Project();
		}

		public object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
