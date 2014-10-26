
namespace Concord.Logging
{
    public static class EnumUtility
    {
        /// <summary>
        /// parse given string into EnvironmentType
        /// </summary>
        /// <param name="environment">the environment string to parse</param>
        public static EnvironmentType ParseEnvironmentType(string environment)
        {
            var result = EnvironmentType.NotSpecified;

            switch (environment.ToLower())
            {
                case "dev":
                case "development":
                    result = EnvironmentType.Development;
                    break;

                case "qa":
                    result = EnvironmentType.QA;
                    break;

                case "staging":
                    result = EnvironmentType.Staging;
                    break;

                case "prod":
                case "release":
                case "production":
                    result = EnvironmentType.Production;
                    break;
            }

            return result;
        }
    }
}
