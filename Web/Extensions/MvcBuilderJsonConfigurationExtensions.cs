using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// The <see cref="IMvcBuilder"/> extension for <see cref="JsonOptions"/> configuration.
    /// </summary>
    public static class MvcBuilderJsonConfigurationExtensions
    {
        /// <summary>
        /// Configures the <see cref="System.Text.Json.JsonSerializer"/> to ignore null condition when writing.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder ConfigureJsonSerializerToNotWriteNull(this IMvcBuilder builder) =>
            builder.AddJsonOptions(ConfigureJsonToIgnoreNull);

        private static void ConfigureJsonToIgnoreNull(JsonOptions options) =>
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    }
}
