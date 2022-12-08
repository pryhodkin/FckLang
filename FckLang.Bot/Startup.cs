using FckLang.Bot.Configuration;
using FckLang.Bot.Models;
using FckLang.Bot.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FckLang.Bot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson();

            var options = Configuration.GetSection("Options");

            services.AddOptions()
                .Configure<MongoDbOptions>(options.GetSection(MongoDbOptions.Section))
                .Configure<BotOptions>(options.GetSection(BotOptions.Section))
                .Configure<AdminOptions>(options.GetSection(AdminOptions.Section));

            services.AddTransient<IStorage<Mapper>>(serviceProvicer =>
            {
                var options = serviceProvicer
                    .GetRequiredService<IOptions<MongoDbOptions>>()
                    .Value;

                var connection = MongoClientSettings.FromConnectionString(options.Connection);
                var db = options.Db;
                var mappersCollection = options.MapperCollection;

                return new MongoStorage<Mapper>(connection, db, mappersCollection);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
