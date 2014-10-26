using System.Collections.Generic;

namespace PushUI.Models.Source
{
	public interface IJsonChangeset
	{
		int id { get; set; }
		string committedBy { get; set; }
		IEnumerable<Tracking.JsonWorkItem> workItems { get; set; }
		string branch { get; set; }
	}
}
