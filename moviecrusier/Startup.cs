using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using moviecrusier.Data.Persistence;
using moviecrusier.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace moviecrusier
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
            var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_MOVIE");

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Configuration.GetConnectionString("MovieCruiserDB");
            }

            // ConfigureJwtAuthService(Configuration, services);

            services.AddMvc();

            services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IMovieDbContext, MovieDbContext>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            var option = new DbContextOptionsBuilder<MovieDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var dbContext = new MovieDbContext(option))
            {
                dbContext.Database.EnsureCreated();
            }

            services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new Info { Title = "Movie Cruiser API", Version = "v1", Description = "Movie Cruiser" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Cruiser API");
            });
            app.UseCors("AllowAllHeaders");
        }
    }
}
