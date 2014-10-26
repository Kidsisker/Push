
using Concord.Push.Models.Source;

namespace PushUI.Models.Source
{
	public class JsonMergeRelationship
	{
		public string source { get; set; }
		public string target { get; set; }

		public JsonMergeRelationship(){ }
		public JsonMergeRelationship(MergeRelationship model)
		{
			source = model.Source != null ? model.Source.Name : null;
			target = model.Target != null ? model.Target.Name : null;
		}
		public static explicit operator MergeRelationship(JsonMergeRelationship json)
		{
			return json != null ? new MergeRelationship
				{
					Source = new Branch { Name = json.source },
					Target = new Branch { Name = json.target }
				} : null;
		}

	}
}