using Gama.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Gama.Infrastructure.OptionsSetup
{
    internal class S3OptionsSetup : IConfigureOptions<S3Options>
    {
        private const string SectionName = "Jwt";
        private readonly IConfiguration _configuration;

        public S3OptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(S3Options options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
