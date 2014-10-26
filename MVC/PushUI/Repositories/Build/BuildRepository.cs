using System.Collections.Generic;
using System.Linq;
using Concord.Push.Models.Build;
using Concord.Push.Models.Source;
using Concord.Push.Service.Build;
using PushUI.Models.Build;
using PushUI.Models.Source;
using BLL = Concord.Push.Business.Build;

namespace PushUI.Repositories.Build
{
	public class BuildRepository : IBuildRepository
	{
		public IBuildDataAccess BuildDataAccess { get; set; }

		/// <summary>
		/// default constructor (passes in dataaccess layer dependency)
		/// </summary>
		public BuildRepository(IBuildDataAccess buildDataAccess)
		{
			BuildDataAccess = buildDataAccess;
		}

		public IEnumerable<JsonBuildDetail> QueryJsonBuilds()
		{
			return BLL.Build.QueryBuilds(BuildDataAccess).Select(b => new JsonBuildDetail(b));
		}

		public IEnumerable<JsonQueuedBuild> QueryJsonQueuedBuilds()
		{
			return BLL.Build.QueryQueuedBuilds(BuildDataAccess).Select(b => new JsonQueuedBuild(b));
		}

		public IEnumerable<IQueuedBuild> QueueJsonMergeEnvironmentBuild(JsonMergeRequest model)
		{
			return QueueEnvironmentBuild(model.environment.target.value, true);
		}

		public IEnumerable<IQueuedBuild> QueueEnvironmentBuild(Environment model, bool wait)
		{
			var builds = new List<IQueuedBuild>();
			// todo: Configure this in web.config. Or a database. What?!?
			const string buildQualityDEV = "Deploy to Development";
			const string buildQualityQA = "Deploy to QA";
			switch (model)
			{
				case Environment.Dev:
					builds.Add(QueueBuild("External InterlinkMVC", "Dev_InterlinkMVC", buildQualityDEV, wait));
					builds.Add(QueueBuild("External InterlinkWorkflow", "Dev_InterlinkWorkflow", buildQualityDEV, wait));
					builds.Add(QueueBuild("Interlink", "MarriottDev_Interlink", "Deploy to Marriott Dev", wait));
					builds.Add(QueueBuild("InterlinkAPIWCF", "Dev_InterlinkAPIWCF", buildQualityDEV, wait));
					break;
				case Environment.QA:
					builds.Add(QueueBuild("External InterlinkMVC", "QA_InterlinkMVC", buildQualityQA, wait));
					builds.Add(QueueBuild("External InterlinkWorkflow", "QA_InterlinkWorkflow", buildQualityQA, wait));
					builds.Add(QueueBuild("Interlink", "MarriottQA_Interlink", "Deploy to Marriott QA", wait));
					builds.Add(QueueBuild("InterlinkAPIWCF", "QA_InterlinkAPIWCF", buildQualityQA, wait));
					break;
				case Environment.MarriottQAExt:
					builds.Add(QueueBuild("External InterlinkMVC", "MarriottQAExt_InterlinkMVC", buildQualityQA, wait));
					builds.Add(QueueBuild("External InterlinkWorkflow", "MarriottQAExt_InterlinkWorkflow", buildQualityQA, wait));
					builds.Add(QueueBuild("Interlink", "MarriottQAExt_Interlink", buildQualityQA, false));
					builds.Add(QueueBuild("InterlinkAPIWCF", "MarriottQAExt_InterlinkAPIWCF", buildQualityQA, wait));
					break;
			}

			return builds;
		}

		public IQueuedBuild QueueBuild(string project, string buildDefUri, string quality, bool wait)
		{
			return BLL.Build.QueueBuild(BuildDataAccess, project, buildDefUri, quality, wait);
		}
	}
}