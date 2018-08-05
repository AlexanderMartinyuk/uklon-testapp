using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Services.Implementation;
using WebAPI.Services.Interfaces;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IRegionService, RegionsService>();
            services.AddScoped<ITrafficCache, TrafficCache>();

            if (Configuration["TrafficProvider"].Equals("Stub"))
            {
                services.AddScoped<ITrafficProvider, StubTrafficProvider>();
                services.AddScoped<ITrafficService, SimpleTrafficService>();
            }
            else
            {
                var proxy = Configuration["Proxy"];
                services.AddScoped<ITrafficProvider>(s => new YandexTrafficProvider(proxy, s.GetService<IRegionService>()));
                services.AddScoped<ITrafficService, CachedTrafficService>();
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var trafficRepository = serviceScope.ServiceProvider.GetRequiredService<ITrafficCache>();
                trafficRepository.InitDatabase();
            }
        }
    }
}
