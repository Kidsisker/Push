using System;
using System.Linq;
using System.Web.Mvc;
using Concord.Logging;
using PushUI.Exceptions;
using PushUI.Models.Application.UI;
using PushUI.Models.Build;
using PushUI.Models.Source;
using PushUI.Repositories.Build;
using PushUI.Repositories.Team;
using Environment = Concord.Push.Models.Source.Environment;

namespace PushUI.Controllers
{
	public class BuildController : Controller
	{
		readonly IBuildRepository _buildRepository;
		private readonly ITeamRepository _teamRepository;
		readonly ILogger _logger;

		public BuildController(IBuildRepository buildRepository, ITeamRepository teamRepository, ILogger logger)
		{
			_buildRepository = buildRepository;
			_teamRepository = teamRepository;
			_logger = logger;
		}

		/// <summary>
		/// Build Index.
		/// </summary>
		public ActionResult Index() // todo: create query def.
		{
			try
			{
				var model = new BuildManagerViewModel
					{
						BuildQueryViewModel = new BuildQueryViewModel
							{
								ApiPath = Url.Action("GetBuildQueryViewModel", "Build")
							}
					};
				return View("Index", model);
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
			}

			return View("Error");
		}

		public JsonResult GetBuildQueryViewModel(JsonBuildQueryRequest request)
		{
			try
			{
				var model = new JsonBuildQueryViewModel
					{
						getResultsPath = Url.Action("QueryBuilds", "Build"),
						results = _buildRepository.QueryJsonBuilds()
					};

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

		/// <summary>
		/// Query Builds.
		/// </summary>
		public JsonResult QueryBuilds() // todo: create query def.
		{
			try
			{
				var results = _buildRepository.QueryJsonBuilds().OrderByDescending(o => o.finishTime).ToList();
				return new JsonDotNetResult(results);
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
		/// Query Builds.
		/// </summary>
		public JsonResult QueryQueuedBuilds() // todo: create query def.
		{
			try
			{
				var results = _buildRepository.QueryJsonQueuedBuilds().OrderByDescending(o => o.queueTime).ToList();
				return new JsonDotNetResult(results);
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
		/// Build Environment.
		/// </summary>
		public ActionResult Queue(string project, string environment)
		{
			try
			{
				var teamProject = _teamRepository.GetProject(project);
				var mergeEnvironment = teamProject.MergeEnvironments.FirstOrDefault(m => string.Compare(m.Name, environment, StringComparison.OrdinalIgnoreCase) == 0);
				if (mergeEnvironment == null)
					return new JsonDotNetResult(null);

				var model = new JsonEnvironment
					{
						value = (Environment)Enum.Parse(typeof(Environment), mergeEnvironment.Name, true)
					};
				return BuildEnvironment(model);
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
			}

			return View("Error");
		}

		/// <summary>
		/// Build Merge Target Environment.
		/// </summary>
		public JsonResult BuildMergeEnvironment(JsonMergeRequest model)
		{
			try
			{
				var builds = _buildRepository.QueueJsonMergeEnvironmentBuild(model);
				return new JsonDotNetResult(builds);
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
			}

			return new JsonDotNetResult(model);
		}

		/// <summary>
		/// Build Environment.
		/// </summary>
		public ActionResult BuildEnvironment(JsonEnvironment model)
		{
			try
			{
				_buildRepository.QueueEnvironmentBuild(model.value, true);
				return RedirectToAction("Index");
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
			}

			return RedirectToAction("Index");
		}
	}
}
