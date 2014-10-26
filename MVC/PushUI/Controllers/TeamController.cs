using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Concord.Logging;
using Concord.Push.Models.Team;
using PushUI.Exceptions;
using PushUI.Models.Application.UI;
using PushUI.Models.Team;
using PushUI.Repositories.Team;

namespace PushUI.Controllers
{
	public class TeamController : Controller
	{
		readonly ITeamRepository _teamRepository;
		readonly ILogger _logger;

		public TeamController(ITeamRepository teamRepository, ILogger logger)
		{
			_teamRepository = teamRepository;
			_logger = logger;
		}

		/// <summary>
		/// loads the activity page
		/// </summary>
		public ActionResult Activity()
		{
			try
			{
				var model = new ActivityViewModel
					{
						Project = _teamRepository.GetDefaultProject(),
						ApiPath = Url.Action("GetActivityViewModel", "Team")
					};
				return PartialView("Activity", model);
			}
			catch (BaseException ex)
			{
				_logger.Log(ex);
			}
			catch (Exception ex)
			{
				_logger.Log(ex);
			}

			return PartialView("Activity");
		}

		public JsonResult GetActivityViewModel(JsonActivityRequest request)
		{
			try
			{
				var model = new JsonActivityViewModel(request)
					{
						projects = _teamRepository.GetJsonProjects().ToList(),
						project = _teamRepository.GetJsonProject(request.project.name)
					};
				model.projects.ToList().ForEach(project =>
					{
						var activities = new List<JsonActivity>
							{
								new JsonActivity
									{
										sequence = 0,
										name = "Manage Builds",
										route = Url.Action("Index", "Build"),
										type = new JsonActivityType
											{
												category = ActivityCategory.Build
											}
									}
							}; // todo: add to config
						project.environments.ToList().ForEach(environment => activities.AddRange(new List<JsonActivity>
							{
								new JsonActivity
									{
										sequence = activities.Count(),
										name = string.Format("Merge source code from {0} to {1}", environment.source.name, environment.target.name),
										route = Url.Action("Merge", "Source",
											           new
												           {
													           project = project.name,
													           source = environment.source.name,
													           target = environment.target.name
												           }),
										type = new JsonActivityType
											{
												category = ActivityCategory.Merge
											}
									},
								new JsonActivity
									{
										sequence = activities.Count(),
										name = string.Format("Build & Release {0}", environment.source.name),
										route = Url.Action("Queue", "Build",
											           new
												           {
													           project = project.name,
													           environment = (activities.Count() %2 == 0 ? environment.target.name : environment.source.name)
												           }),
										type = new JsonActivityType
											{
												category = ActivityCategory.Build
											}
									}
							}));
						project.activities = activities;
					});

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
	}
}
