using AspNetCoreRateLimit;
using AutoMapper;
using HackerNews.Api.Filter;
using HackerNews.Data.Repository;
using HackerNews.Domain.Config;
using HackerNews.Domain.Repository;
using HackerNews.Service;
using HackerNews.Service.Interface;
using HackerNews.Service.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace HackerNews.Api
{
    public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			Configuration = configuration;
            Environment = environment;
        }

		public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
		{
            services.AddAutoMapper(typeof(MappingProfile).GetTypeInfo().Assembly);
            services.Configure<HackerNewsConfig>(Configuration);
            services.AddMvc(options =>
                options.Filters.Add(typeof(HttpGlobalExceptionFilter))
            ).AddControllersAsServices()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddSingleton<IStoryService, StoryService>();
            services.AddSingleton<IStoryRepository, StoryRepository>();

            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimit"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hacker News API", Version = "v1" });
            });
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
            app.UseIpRateLimiting();
            app.UseMvc();
            
            var pathBase = Configuration["PATH_BASE"];
            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Hacker News API V1");
                  c.OAuthClientId("HackerNewsSwaggerUI");
                  c.OAuthAppName("Hacker News Swagger UI");
              });
            
        }
	}
}
