using System.Collections.Generic;
using PushUI.Models.Team;

namespace PushUI.Models.Source
{
	public class JsonMergeViewModel
	{
		public IEnumerable<JsonTeamProject> projects { get; set; }
		public JsonTeamProject project { get; set; }
		public JsonMergeEnvironment environment { get; set; }
		public IEnumerable<JsonMergeMethod> mergeMethods { get; set; }
		public JsonMergeMethod mergeMethod { get; set; }
		public JsonMergeMethodOption mergeMethodOption { get; set; }
		public bool autoCommit { get; set; }
		public string getWorkItemsPath { get; set; }
		public string getChangesetsPath { get; set; }
		public string getMergeCandidatesPath { get; set; }
		public string getMigrationScriptsPath { get; set; }
		public string commitChangesetPath { get; set; }
		public string mergeChangesetPath { get; set; }
		public string buildEnvironmentPath { get; set; }

		public JsonMergeViewModel() { }
		public JsonMergeViewModel(JsonMergeRequest model)
		{
			project = new JsonTeamProject(model.project);
			environment = model.environment;
		}
	}
}