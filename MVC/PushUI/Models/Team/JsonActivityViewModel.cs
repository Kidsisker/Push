using System.Collections.Generic;

namespace PushUI.Models.Team
{
	public class JsonActivityViewModel
	{
		public IEnumerable<JsonTeamProject> projects { get; set; }
		public JsonTeamProject project { get; set; }
		public JsonActivity activity { get; set; }

		public JsonActivityViewModel() { }
		public JsonActivityViewModel(JsonActivityRequest model)
		{
			project = new JsonTeamProject(model.project);
		}
	}
}