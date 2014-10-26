using System;
using System.Web.Mvc;
using Concord.Logging;
using PushUI.Exceptions;
using PushUI.Models.Application.UI;
using PushUI.Models.Tracking;
using PushUI.Repositories.Tracking;

namespace PushUI.Controllers
{
	public class TrackingController : Controller
	{
		readonly ITrackingRepository _trackingRepository;
		readonly ILogger _logger;

		public TrackingController(ITrackingRepository trackingRepository, ILogger logger)
		{
			_trackingRepository = trackingRepository;
			_logger = logger;
		}

		/// <summary>
		/// Return ViewModel for Displaying default options available when merging to QA from Dev
		/// </summary>
		public JsonResult GetWorkItems(JsonWorkItemRequest request)
		{
			try
			{
				return new JsonDotNetResult(_trackingRepository.GetJsonWorkItems(request));
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
		/// Create Merge Tasks and return to client.
		/// </summary>
		public JsonResult GetWorkItemsWithMigrationScripts(JsonWorkItemRequest request)
		{
			try
			{
				return new JsonDotNetResult(_trackingRepository.GetWorkItemsWithMigrationScripts(request));
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
		/// Create Merge Tasks and return to client.
		/// </summary>
		public JsonResult CreateMergeTasks(JsonCreateWorkItemRequest request)
		{
			try
			{
				return new JsonDotNetResult(_trackingRepository.CreateMergeTasks(request));
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
