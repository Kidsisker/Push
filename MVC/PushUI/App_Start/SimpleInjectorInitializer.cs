using Concord.Logging;
using PushUI.Repositories.Build;
using PushUI.Repositories.Source;
using PushUI.Repositories.Team;
using PushUI.Repositories.Tracking;

[assembly: WebActivator.PostApplicationStartMethod(typeof(PushUI.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace PushUI.App_Start
{
	using System.Reflection;
	using System.Web.Mvc;

	using SimpleInjector;
	using SimpleInjector.Integration.Web.Mvc;

	public static class SimpleInjectorInitializer
	{
		/// <summary>Initialize the container and register it as MVC Dependency Resolver.</summary>
		public static void Initialize()
		{
			// Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
			var container = new Container();
			
			InitializeContainer(container);

			container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
			
			container.Verify();
			
			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
		}
	 
		/// <summary>
		/// register the services we'll be using in the application
		/// </summary>
		/// <param name="container">the container to register against</param>
		private static void InitializeContainer(Container container)
		{
			// Source.
			container.Register<Concord.Push.Service.Source.ISourceDataAccess, Concord.Push.Data.Source.TfsSourceDataAccess>();
			container.Register<ISourceRepository>(() =>
				{
					var repository = new SourceRepository(container.GetInstance<Concord.Push.Service.Source.ISourceDataAccess>());
					return repository;
				});
			// Team.
			container.Register<Concord.Push.Service.Team.ITeamDataAccess, Concord.Push.Data.Team.TfsTeamDataAccess>();
			container.Register<ITeamRepository>(() =>
			{
				var repository = new TeamRepository(container.GetInstance<Concord.Push.Service.Team.ITeamDataAccess>());
				return repository;
			});
			// Tracking.
			container.Register<Concord.Push.Service.Tracking.ITrackingDataAccess, Concord.Push.Data.Tracking.TfsTrackingDataAccess>();
			container.Register<ITrackingRepository>(() =>
			{
				var repository = new TrackingRepository(container.GetInstance<Concord.Push.Service.Tracking.ITrackingDataAccess>());
				return repository;
			});
			// Build.
			container.Register<Concord.Push.Service.Build.IBuildDataAccess, Concord.Push.Data.Build.TfsBuildDataAccess>();
			container.Register<IBuildRepository>(() =>
			{
				var repository = new BuildRepository(container.GetInstance<Concord.Push.Service.Build.IBuildDataAccess>());
				return repository;
			});

			container.Register<ILogger>(() =>
				{
					var logger = new Concord.Logging.Loggers.EFLogger(
						new ApplicationDetails
						{
							ApplicationName = Config.ConfigSettings.ApplicationName,
							ApplicationVersion = Config.ConfigSettings.ApplicationVersion,
							Environment = Config.ConfigSettings.Environment
						});
					return logger;
				});
		}
	}
}