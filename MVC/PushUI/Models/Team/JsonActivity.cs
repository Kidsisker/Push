using Concord.Push.Models.Team;

namespace PushUI.Models.Team
{
	public class JsonActivity
	{
		public int id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string route { get; set; }
		public JsonActivityType type { get; set; }
		public int sequence { get; set; }

		public JsonActivity() { }
		public JsonActivity(IActivity model)
		{
			id = model.Id;
			name = model.Name;
			description = model.Description;
			type = new JsonActivityType(model.Type);
		}

		public static explicit operator Activity(JsonActivity json)
		{
			return new Activity
				{
					Id = json.id,
					Name = json.name,
					Description = json.description,
					Type = (ActivityType)json.type
				};
		}
	}
}