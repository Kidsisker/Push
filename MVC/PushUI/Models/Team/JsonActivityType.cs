using System.Collections.Generic;
using Concord.Push.Models.Team;

namespace PushUI.Models.Team
{
	public class JsonActivityType
	{
		public int id { get; set; }
		public string name { get; set; }
		public ActivityCategory category { get; set; }
		public IEnumerable<string> tags { get; set; } 

		public JsonActivityType() { }
		public JsonActivityType(IActivityType model)
		{
			id = model.Id;
			name = model.Name;
			category = model.Category;
			tags = model.Tags;
		}

		public static explicit operator ActivityType(JsonActivityType json)
		{
			return new ActivityType
				{
					Id = json.id,
					Name = json.name,
					Category = json.category,
					Tags = json.tags
				};
		}
	}
}
