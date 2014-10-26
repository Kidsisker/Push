using System.Collections.Generic;
using System.Linq;
using Concord.Push.Models.Source;

namespace PushUI.Models.Source
{
	public class JsonEnvironment
	{
		public Environment value { get; set; }
		public string name
		{
			get { return value.ToString(); }
		}
	}

	public class JsonMergeEnvironment
	{
		public string name { get; set; }
		public JsonEnvironment source { get; set; }
		public JsonEnvironment target { get; set; }
		public IEnumerable<string> states { get; set; }
		public IEnumerable<JsonMergeRelationship> relationships { get; set; }

		public JsonMergeEnvironment(){ }
		public JsonMergeEnvironment(MergeEnvironment model)
		{
			name = model.Name;
			source = new JsonEnvironment { value = model.Source };
			target = new JsonEnvironment { value = model.Target };
			states = model.AllowedStates;
			relationships = model.Relationships != null ? model.Relationships.Select(b => new JsonMergeRelationship(b)) : null;
		}
		public static explicit operator MergeEnvironment(JsonMergeEnvironment json)
		{
			return json != null ? new MergeEnvironment
				{
					Name = json.name,
					Source = json.source != null ? json.source.value : Environment.Unknown,
					Target = json.target != null ? json.target.value : Environment.Unknown,
					AllowedStates = json.states != null ? json.states.Select(s => s) : null,
					Relationships = json.relationships != null ? json.relationships.Select(b => new MergeRelationship
						{
							Source = new Branch { Name = b.source },
							Target = new Branch { Name = b.target }
						}) : null
				} : null;
		}
	}
}