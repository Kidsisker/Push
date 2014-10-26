using System.Linq;
using System.Collections.Generic;
using Concord.Push.Models.Source;
using Concord.Push.Models.Team;
using PushUI.Models.Source;

namespace PushUI.Models.Team
{
	public class JsonTeamProject : IJsonProject
	{
		public int id { get; set; }
		public string name { get; set; }
		public bool isDefault { get; set; }
		public IEnumerable<JsonMergeEnvironment> environments { get; set; }
		public IEnumerable<JsonActivity> activities { get; set; }

		public JsonTeamProject(){ }
		public JsonTeamProject(IJsonProject model)
		{
			id = model.id;
			name = model.name;
			isDefault = model.isDefault;
		}
		public JsonTeamProject(TeamProject model)
		{
			id = model.Id;
			name = model.Name;
			isDefault = model.IsDefault;
			environments = model.MergeEnvironments != null ? model.MergeEnvironments.Select(m => new JsonMergeEnvironment(m)) : null;
			activities = model.Activities != null ? model.Activities.Select(m => new JsonActivity(m)) : null;
		}

		public static explicit operator TeamProject(JsonTeamProject json)
		{
			return new TeamProject
			{
				Id = json.id,
				Name = json.name,
				IsDefault = json.isDefault,
				MergeEnvironments = json.environments != null ? json.environments.Select(m => (MergeEnvironment)m) : null,
				Activities = json.activities != null ? json.activities.Select(m => (Activity)m) : null
			};
		}
	}
}
