using System;
using System.Collections.Generic;
using System.Linq;
using Concord.Push.Data.Team;
using Concord.Push.Models.Source;
using Concord.Push.Service.Build;
using Microsoft.TeamFoundation.Build.Client;
using Concord.Push.Models.Build;
using IBuildRequest = Concord.Push.Models.Build.IBuildRequest;
using IBuildDefinition = Concord.Push.Models.Build.IBuildDefinition;
using IBuildDetail = Concord.Push.Models.Build.IBuildDetail;
using IQueuedBuild = Concord.Push.Models.Build.IQueuedBuild;
using BuildStatus = Concord.Push.Models.Build.BuildStatus;

namespace Concord.Push.Data.Build
{
	/// <summary>
	/// data access class to get source data from the Tfs API 
	/// </summary>
	public class TfsBuildDataAccess : IBuildDataAccess
	{
		public IEnumerable<IBuildDetail> QueryBuilds()
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var buildServer = tfs.GetService<IBuildServer>();
			var spec = buildServer.CreateBuildDetailSpec("*", "*");
			spec.MinFinishTime = DateTime.Today.AddDays(-7);
			spec.MaxFinishTime = DateTime.Today.AddDays(1);
			spec.RequestedFor = tfs.AuthorizedIdentity.UniqueName;
			var buildResults = buildServer.QueryBuilds(spec);
			return buildResults.Builds.Select(MapBuild).ToList();
		}

		public IEnumerable<IQueuedBuild> QueryQueuedBuilds()
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var buildServer = tfs.GetService<IBuildServer>();
			var spec = buildServer.CreateBuildQueueSpec("*", "*");
			var buildResults = buildServer.QueryQueuedBuilds(spec);
			return buildResults.QueuedBuilds.Select(MapQueuedBuild).ToList();
		}

		public IQueuedBuild QueueBuild(string project, string buildDefinition, string quality, bool wait)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var buildServer = tfs.GetService<IBuildServer>();
			var buildDef = buildServer.GetBuildDefinition(project, buildDefinition);
			var buildRequest = buildServer.CreateBuildRequest(buildDef.Uri);
			return QueueTfsBuild(buildRequest, quality, wait);
		}

		public IQueuedBuild QueueBuild(IBuildRequest buildRequest)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			var buildServer = tfs.GetService<IBuildServer>();
			var tfsBuildRequest = buildServer.CreateBuildRequest(buildRequest.BuildDefinitionUri);
			return QueueTfsBuild(tfsBuildRequest, buildRequest.Quality, buildRequest.Wait);
		}

		internal static IQueuedBuild QueueTfsBuild(Microsoft.TeamFoundation.Build.Client.IBuildRequest buildRequest, string quality, bool wait)
		{
			var tfs = TfsTeamDataAccess.GetTfsTeamProjectCollection();
			// Create a build request for the gated check-in build
			var buildServer = tfs.GetService<IBuildServer>();

			//Queue the build request
			var queuedBuild = buildServer.QueueBuild(buildRequest);

			if (wait)
			{
				// Wait for the build to complete.
				while (!queuedBuild.Build.BuildFinished)
				{
					// Get the latest status of the build, and pause to yield CPU
					queuedBuild.Refresh(QueryOptions.Process);
					System.Threading.Thread.Sleep(1000);
				}
			}
			if (queuedBuild.Build == null)
				return MapQueuedBuild(queuedBuild);

			if (queuedBuild.Build.Status == Microsoft.TeamFoundation.Build.Client.BuildStatus.Succeeded)
			{
				queuedBuild.Build.RefreshAllDetails();

				if (!string.IsNullOrEmpty(quality))
				{
					queuedBuild.Build.Quality = quality; //todo: implement Release data access
					queuedBuild.Build.Save();
				}
			}
			var changesetSummaries = InformationNodeConverters.GetAssociatedChangesets(queuedBuild.Build);
			var build = MapQueuedBuild(queuedBuild);
			build.Build.Changesets = changesetSummaries.Select(c => new Changeset
				{
					Id = c.ChangesetId
				});
			
			return build;
		}

		private static IQueuedBuild MapQueuedBuild(Microsoft.TeamFoundation.Build.Client.IQueuedBuild tfsBuild)
		{
			return tfsBuild != null ? new QueuedBuild
				{
					Id = tfsBuild.Id,
					QueuePosition = tfsBuild.QueuePosition,
					QueueTime = tfsBuild.QueueTime,
					RequestedBy = tfsBuild.RequestedBy,
					RequestedByDisplayName = tfsBuild.RequestedByDisplayName,
					Build = MapBuild(tfsBuild.Build)
				} : null;
		}

		private static IBuildDetail MapBuild(Microsoft.TeamFoundation.Build.Client.IBuildDetail tfsBuild)
		{
			return tfsBuild != null ? new BuildDetail
				{
					BuildDefinition = MapBuildDefinition(tfsBuild.BuildDefinition),
					BuildDefinitionUri = tfsBuild.BuildDefinitionUri,
					BuildNumber = tfsBuild.BuildNumber,
					FinishTime = tfsBuild.FinishTime,
					BuildFinished = tfsBuild.BuildFinished,
					Quality = tfsBuild.Quality,
					RequestedBy = tfsBuild.RequestedBy,
					RequestedFor = tfsBuild.RequestedFor,
					StartTime = tfsBuild.StartTime,
					Status = (BuildStatus) ((int) tfsBuild.Status)
				} : null;
		}

		private static IBuildDefinition MapBuildDefinition(Microsoft.TeamFoundation.Build.Client.IBuildDefinition tfsBuildDefinition)
		{
			return new BuildDefinition
			{
				Name = tfsBuildDefinition.Name
			};
		}
	}
}
