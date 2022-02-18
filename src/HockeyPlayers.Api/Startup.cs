using HockeyPlayers.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mindscape.Raygun4Net.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HockeyPlayers.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            string connectionString = Configuration.GetConnectionString("TemplateContext");

            var dbName = $"Template_{DateTime.Now.ToFileTimeUtc()}";

            services.AddDbContext<TemplateContext>(options => options.UseInMemoryDatabase(dbName));

            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString = connectionString;
            //    options.SchemaName = "Cache";
            //    options.TableName = "Cache";
            //});

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(appSettings.CorsSettings.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(86400)));
            });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.UseInlineDefinitionsForEnums();
                options.DescribeAllParametersInCamelCase();
                options.CustomSchemaIds(x => x.ToString());
            });

            services.AddRaygun(Configuration);

            services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TemplateContext>();
            PopulateData(context);

            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRaygun();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Coucou les coucous");
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void PopulateData(TemplateContext context)
        {
            var players = new List<Player>
            {
                new Player { Id = 1, JerseyNumber = 13, FirstName = "Yanik", LastName = "Cadieux", Position = Position.LW, Active = true },
                new Player { Id = 2, JerseyNumber = 99, FirstName = "Wayne", LastName = "Gretzky", Position = Position.C , Active = false},
                new Player { Id = 3, JerseyNumber = 87, FirstName = "Sidney", LastName = "Crosby", Position = Position.C, Active = true },
                new Player { Id = 4, JerseyNumber = 31, FirstName = "Carey", LastName = "Price", Position = Position.G, Active = true },
                new Player { Id = 5, JerseyNumber = 6, FirstName = "Shea", LastName = "Weber", Position = Position.D, Active = false },
            };

            context.Players.AddRange(players);
            context.SaveChanges();
        }
    }
}
