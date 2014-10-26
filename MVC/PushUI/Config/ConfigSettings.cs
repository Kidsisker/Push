using System;
using System.Configuration;

namespace PushUI.Config
{
    /// <summary>
    /// application configuration settings pulled from the db, the config or anywhere else
    /// </summary>
    public static class ConfigSettings
    {
        /// <summary>
        /// the name of this application
        /// </summary>
        public static string ApplicationName
        {
            get { return "PushUI"; }
        }

        /// <summary>
        /// the version of this application
        /// </summary>
        public static string ApplicationVersion
        {
            get { return "1.0.0.0"; }
        }

        /// <summary>
        /// the current environment specified in the config
        /// </summary>
        public static Concord.Logging.EnvironmentType Environment
        {
            get
            {
                var environment = Concord.Logging.EnvironmentType.NotSpecified;

                var tmp = ConfigurationManager.AppSettings["EnvironmentLabel"];
                if (!String.IsNullOrEmpty(tmp))
                    environment = Concord.Logging.EnumUtility.ParseEnvironmentType(tmp);

                return environment;
            }
        }
    }
}