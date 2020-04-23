namespace PhungDKH.Microservice.Api
{
    using AutoMapper;
    using Coursepad.Tag.Api.Infrastructure.Filters;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using PhungDKH.EventBusRabbitMQ;
    using PhungDKH.Microservice.Domain.Entities.Contexts;
    using PhungDKH.Microservice.Service.Common;
    using RabbitMQ.Client;

    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        public static IConfigurationRoot Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Microservice API",
                    Description = "Microservice API",
                    TermsOfService = null,
                    Contact = new OpenApiContact { Name = "DINH KHAC HOAI PHUNG", Email = "phungdkh@gmail.com", Url = null },
                });

                c.DescribeAllParametersInCamelCase();
            });

            services.AddSingleton(Configuration);

            // Entity framework
            string msSqlConnectionString = Configuration.GetValue<string>("database:msSql:connectionString");
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(
                    msSqlConnectionString,
                    options =>
                    {
                        options.EnableRetryOnFailure();
                    }));

            // Health checks
            services.AddHealthChecks()
                .AddSqlServer(msSqlConnectionString);

            services.AddMediatR(typeof(BaseRequestModel).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = Configuration.GetValue<string>("rabbitmq:hostname"),
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(Configuration.GetValue<string>("rabbitmq:username")))
                {
                    factory.UserName = Configuration.GetValue<string>("rabbitmq:username");
                }

                if (!string.IsNullOrEmpty(Configuration.GetValue<string>("rabbitmq:password")))
                {
                    factory.Password = Configuration.GetValue<string>("rabbitmq:password");
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration.GetValue<string>("rabbitmq:retycount")))
                {
                    retryCount = int.Parse(Configuration.GetValue<string>("rabbitmq:retycount"));
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservice API V1");
            });

            // auto migration
            this.RunMigration(app);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        private void RunMigration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
            }
        }
    }
}
