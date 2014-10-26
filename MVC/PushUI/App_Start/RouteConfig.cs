using System.Web.Mvc;
using System.Web.Routing;

namespace PushUI.App_Start
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Merge",
				url: "{project}/{controller}/{action}/{source}/{target}",
				defaults: new
				{
					project = "Marathon"
				},
				constraints: new
				{
					controller = "Source",
					action = "Merge"
				}
			);

			routes.MapRoute(
				name: "Build",
				url: "{project}/{controller}/{action}/{environment}",
				defaults: new
				{
					project = "Marathon"
				},
				constraints: new
				{
					controller = "Build",
					action = "Queue"
				}
			);


			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}