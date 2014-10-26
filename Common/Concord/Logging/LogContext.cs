using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Concord.Logging.Models;

namespace Concord.Logging
{
    [DbConfigurationType(typeof(LogConfiguration))]
    public class LogContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Browser> Browsers { get; set; }
        public DbSet<Environment> Environments { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<XmlLog> XmlLogs { get; set; }

        public DbSet<Host> Hosts { get; set; }

        public DbSet<Request> Requests { get; set; }


        public LogContext()
            : base("LogContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}
