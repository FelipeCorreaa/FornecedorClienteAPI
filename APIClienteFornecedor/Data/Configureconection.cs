using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace APIClienteFornecedor
{
    public class ConfigureConnection
    {
        private readonly string _connectionString;

        public ConfigureConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
