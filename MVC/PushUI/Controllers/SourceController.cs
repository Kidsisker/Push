using System;
using System.Linq;
using System.Web.Mvc;
using Concord.Logging;
using PushUI.Exceptions;
using PushUI.Models.Application.UI;
using PushUI.Models.Source;
using PushUI.Models.Team;
using PushUI.Repositories.Source;
using PushUI.Repositories.Team;
using PushUI.Repositories.Tracking;

namespace PushUI.Controllers
{
	public class SourceController : Controller
	{
		readonly ITeamRepository _teamRepository ;
		readonly ISourceRepository _sourceRepository;
		readonly ITrackingRepository _trackingRepository;
		readonly ILogger _logger;

		public SourceController(ISourceRepository sourceRepository, ITeamRepository teamRepository, ITrackingRepository trackingRepository, ILogger logger)
		{
			_sourceRepository = sourceRepository;
			_teamRepository = teamRepository;
			_trackingRepository = trackingRepository;
			_logger = logger;
		}

		/// <summary>
		/// Request the Merge Dashboard.
		/// </summary>
		/// <param name="project"></param>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public ActionResult Merge(string project, string source, string target)
		{
			var teamProject = _teamRepository.GetProject(project);
			var model = new MergeViewModel
				{
					ApiPath = Url.Action("GetMergeViewModel", "Source"),
					Project = teamProject,
					Environment = teamProject.MergeEnvironments
						.FirstOrDefault(m => string.Compare(m.Source.ToString(), source, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(m.Target.ToString(), target, StringComparison.OrdinalIgnoreCase) == 0)
				};
			return model.Environment == null ? View("Error") : View("Index", model);
		}

		/// <summary>
		/// Merge Changeset.
		/// </summary>
		public JsonResult GetChangesets(JsonMergeViewModel model)
		{
			try
			{
				return new JsonDotNetResult(_sourceRepository.GetJsonChangesetsByWorkItemState(model).OrderBy(o => o.id).ToList());
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
			}

			return new JsonDotNetResult(null);
		}

		/// <summary>
		/// Merge Changeset.
		/// </summary>
		public JsonResult MergeChangeset(JsonMergeCandidateChangeset model)
		{
			try
			{
				model.status = _sourceRepository.MergeJsonChangeset(model);
				if (model.status.numFailures == 0 && model.status.numConflicts == 0 && model.autoCommit)
					model.mergedChangeset = _sourceRepository.JsonCommitChangeset(model, null);
				return new JsonDotNetResult(model);
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
				if (model.status == null) model.status = new JsonMergeStatus();
				model.status.numFailures++;
				model.status.message += ex.FriendlyMessage;
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
				if (model.status == null) model.status = new JsonMergeStatus();
				model.status.numFailures++;
				model.status.message += ex.Message;
			}

			return new JsonDotNetResult(model);
		}

		/// <summary>
		/// Commit Changeset.
		/// </summary>
		public JsonResult CommitChangeset(JsonMergeCandidateChangeset model)
		{
			try
			{
				model.mergedChangeset = _sourceRepository.JsonCommitChangeset(model, null);
				return new JsonDotNetResult(model);
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
				if (model.status == null) model.status = new JsonMergeStatus();
				model.status.numFailures++;
				model.status.message += ex.FriendlyMessage;
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
				if (model.status == null) model.status = new JsonMergeStatus();
				model.status.numFailures++;
				model.status.message += ex.Message;
			}

			return new JsonDotNetResult(model);
		}

		/// <summary>
		/// Commit Changeset.
		/// </summary>
		public JsonResult CommitPending(JsonMergeCandidateChangeset model)
		{
			try
			{
				model.mergedChangeset = _sourceRepository.JsonCommitPending(null, model.workItems);
				return new JsonDotNetResult(model);
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
				if (model.status == null) model.status = new JsonMergeStatus();
				model.status.numFailures++;
				model.status.message += ex.FriendlyMessage;
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
				if (model.status == null) model.status = new JsonMergeStatus();
				model.status.numFailures++;
				model.status.message += ex.Message;
			}

			return new JsonDotNetResult(model);
		}

		public JsonResult GetMergeViewModel(JsonMergeRequest request)
		{
			try
			{
				var model = new JsonMergeViewModel(request)
				{
					projects = _teamRepository.GetJsonProjects().ToList(), 
					project = _teamRepository.GetJsonProject(request.project.name),
					mergeMethods = _trackingRepository.GetJsonMergeMethods(request.project, request.environment),
					getWorkItemsPath = Url.Action("GetWorkItems", "Tracking"),
					getChangesetsPath = Url.Action("GetChangesets", "Source"),
					getMergeCandidatesPath = Url.Action("GetMergeCandidates", "Source"),
					getMigrationScriptsPath = Url.Action("GetWorkItemsWithMigrationScripts", "Tracking"),
					commitChangesetPath = Url.Action("CommitChangeset", "Source"),
					mergeChangesetPath = Url.Action("MergeChangeset", "Source"),
					buildEnvironmentPath = Url.Action("BuildMergeEnvironment", "Build")
				};
				model.mergeMethod = model.mergeMethods != null ? model.mergeMethods.FirstOrDefault(m => m.selected) : null;
				if (model.mergeMethod != null && model.mergeMethod.options.Any())
				{
					model.mergeMethodOption = model.mergeMethod.options.FirstOrDefault(m => m.selected);
					if (model.mergeMethodOption != null)
						model.mergeMethodOption.project = new JsonProject((model.project != null ? model.projects.ToList().Find(p => string.Compare(p.name, model.project.name, StringComparison.OrdinalIgnoreCase) == 0) : null) 
							?? model.projects.ToList().Find(p => p.isDefault) 
							?? model.projects.FirstOrDefault());
				}

				return new JsonDotNetResult(model);
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
			}

			return new JsonDotNetResult(null);
		}

		public JsonResult GetMergeCandidates(JsonMergeRequest model)
		{
			try
			{
				return new JsonDotNetResult(_sourceRepository.GetJsonMergeCandidates(model.project, model.environment).OrderBy(o => o.id));
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
			}

			return new JsonDotNetResult(null);
		}
	}
}
