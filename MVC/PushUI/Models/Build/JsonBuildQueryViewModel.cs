using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushUI.Models.Build
{
	public class JsonBuildQueryViewModel
	{
		public IEnumerable<JsonBuildDetail> results { get; set; } 
		public string getResultsPath { get; set; }
	}
}