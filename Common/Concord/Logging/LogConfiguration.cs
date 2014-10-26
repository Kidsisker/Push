using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Concord.Logging
{
    public class LogConfiguration : DbConfiguration
    {
        public LogConfiguration()
        {
            // Use LocalDB for Entity Framework by default
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));

        }
    }
}
