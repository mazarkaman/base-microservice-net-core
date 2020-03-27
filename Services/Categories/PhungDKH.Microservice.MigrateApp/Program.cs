namespace PhungDKH.Microservice.Data
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using PhungDKH.Microservice.Domain.Entities.Contexts;
    using PhungDKH.Microservice.Domain.Services;

    public class Program
    {
        // Using IHostedService in console app.
        // https://www.stevejgordon.co.uk/using-generic-host-in-dotnet-core-console-based-microservices
        static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            await builder.RunConsoleAsync();
        }

        /// <summary>
        ///   Entity Framework Add-Migration and other commands run <see cref="CreateHostBuilder(string[])"/> to get the <see cref="DbContext"/> configuration.
        ///   See <see href="https://github.com/aspnet/EntityFrameworkCore/issues/8332#issuecomment-415512525">The WebHost pattern works with generic host</see>.
        /// </summary>
        /// <param name="args">Arguments are loaded as configuration. It is also required to satisfy the method signature that Entity Framework looks for.</param>
        /// <returns>A configured <see cref="IHostBuilder" />.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();

                        if (args != null)
                        {
                            config.AddCommandLine(args);
                        }
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddOptions();

                        string msSqlConnectionString = hostContext.Configuration.GetValue<string>("database:msSql:connectionString");
                        services.AddDbContext<AppDbContext>(opt =>
                        opt.UseSqlServer(
                            msSqlConnectionString,
                            options =>
                            {
                                options.MigrationsAssembly(typeof(Program).Assembly.FullName);
                            }));

                        services.AddSingleton<IHostedService, MigrateAppService>();
                    })
                    .ConfigureLogging((hostContext, logging) =>
                    {
                        logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                    });
        }
    }
}
