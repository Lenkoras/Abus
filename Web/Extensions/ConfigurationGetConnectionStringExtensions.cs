namespace Microsoft.Extensions.Configuration
{
    using System.Data.Common;

    public static class ConfigurationGetConnectionStringExtensions
    {
        /// <summary>
        /// <para>Gets the connection config from a <paramref name="configuration"/> by "DbConnectionConfig" key and build it in the connection string.</para>
        /// If configuration is not found then instance of the <see cref="InvalidOperationException"/> will be thrown.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>The connection string.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static string GetConnectionString(this IConfiguration configuration) =>
            GetConnectionString(configuration, currentReferenceDepth: 0);

        private static string GetConnectionString(this IConfiguration configuration, int currentReferenceDepth)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            if (currentReferenceDepth < 0)
            {
                throw new ArgumentException();
            }
            const int MaxReferenceDepth = 32;
            if (currentReferenceDepth > MaxReferenceDepth)
            {
                throw new InvalidOperationException("Possible recursive dependency. Too big depth of configuration path reference.");
            }

            IConfigurationSection dbConnectionConfig = configuration.GetRequiredSection("DbConnectionConfig");

            IConfigurationSection pathSection = dbConnectionConfig.GetSection("Path");

            var path = pathSection.Value;
            if (path is not null)
            {
                return GetConnectionString(
                    new ConfigurationBuilder()
                        .AddJsonFile(path)
                        .Build(), currentReferenceDepth: currentReferenceDepth + 1);
            }

            return BuildConnectionString(dbConnectionConfig);
        }

        private static string BuildConnectionString(IConfigurationSection dbConnectionConfig)
        {
            var dbConnectionStringBuilder = new DbConnectionStringBuilder();
            foreach (var section in dbConnectionConfig.GetChildren().Reverse())
            {
                if (section.Value is not null)
                {
                    dbConnectionStringBuilder.Add(section.Key, section.Value!);
                }
            }
            return dbConnectionStringBuilder.ConnectionString;
        }
    }
}