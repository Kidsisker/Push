using Concord.Push.Models.Team;

namespace PushUI.Models.Team
{
	public class JsonProject : IJsonProject
	{
		public int id { get; set; }
		public string name { get; set; }
		public bool isDefault { get; set; }

		public JsonProject(){ }
		public JsonProject(IJsonProject model)
		{
			id = model.id;
			name = model.name;
			isDefault = model.isDefault;
		}
		public JsonProject(IProject model)
		{
			id = model.Id;
			name = model.Name;
			isDefault = model.IsDefault;
		}

		public static explicit operator Project(JsonProject json)
		{
			return new Project
			{
				Id = json.id,
				Name = json.name,
				IsDefault = json.isDefault
			};
		}
	}
}
