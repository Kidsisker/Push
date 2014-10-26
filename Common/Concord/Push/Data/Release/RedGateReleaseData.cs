using System.Data.SqlClient;
using RedGate.SQLCompare.Engine;
using RedGate.Shared.SQL.ExecutionBlock;

namespace Concord.Push.Data.Release
{
	public class RedGateReleaseData
	{
		internal Differences RedGateCompare(string sourceServer, string sourceDatabase, string targetServer, string targetDatabase)
		{
			using (Database sourceDB = new Database(), targetDB = new Database())
			{
				var sourceConnectionProperties = new ConnectionProperties(sourceServer, sourceDatabase);
				var targetConnectionProperties = new ConnectionProperties(targetServer, targetDatabase);

				// Connect to the two databases and read the schema
				try
				{
					sourceDB.Register(sourceConnectionProperties, Options.Default);
				}
				catch (SqlException e)
				{
					return null;
				}
				try
				{
					targetDB.Register(targetConnectionProperties, Options.Default);
				}
				catch (SqlException e)
				{
					return null;
				}

				// Compare and return differences.
				return sourceDB.CompareWith(targetDB, Options.Default);
			}
		}

		internal void RedGateReleaseDifferences(string sourceServer, string sourceDatabase, string targetServer, string targetDatabase)
		{
			var differences = RedGateCompare(sourceServer, sourceDatabase, targetServer, targetDatabase);

			// Select the differences to include in the synchronization. In this case, we're using all differences.
			foreach (var difference in differences)
			{
				difference.Selected = true;
			}

			var work = new Work();

			// Calculate the work to do using sensible default options
			// The script is to be run on WidgetProduction so the runOnTwo parameter is true
			work.BuildFromDifferences(differences, Options.Default, true);

			// Disposing the execution block when it's not needed any more is important to ensure
			// that all the temporary files are cleaned up
			using (var block = work.ExecutionBlock)
			{
				// Finally, use a BlockExecutor to run the SQL against the target database
				var executor = new BlockExecutor();
				executor.ExecuteBlock(block, targetServer, targetDatabase);
			}
		}

		internal Models.Source.DifferenceType MapDifferenceType(DifferenceType type)
		{
			switch (type)
			{
				case DifferenceType.OnlyIn1:
					return Models.Source.DifferenceType.OnlyInSource;
				case DifferenceType.OnlyIn2:
					return Models.Source.DifferenceType.OnlyInSource;
				case DifferenceType.Different:
					return Models.Source.DifferenceType.Different;
				case DifferenceType.Equal:
					return Models.Source.DifferenceType.Equal;
				default:
					return Models.Source.DifferenceType.Unknown;
			}
		}
	}
}
