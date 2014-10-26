using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushUI.Models.Team
{
	public class JsonActivityRequest : IJsonRequest
	{
		public JsonProject project { get; set; }
		public string apiPath { get; set; }

		public JsonActivityRequest() { }
		public JsonActivityRequest(ActivityViewModel model)
		{
			project = new JsonProject(model.Project);
			apiPath = model.ApiPath;
		}
	}
}