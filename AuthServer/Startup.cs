using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthServer.Data.Persistence;
using AuthServer.Services;
using Microsoft.EntityFrameworkCore;

namespace AuthServer
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ConnectionString from Environment variable
            var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_AUTH");
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Configuration.GetConnectionString("AuthDB");
            }
            services.AddMvc();
            // Dependency Injection from Services Configuration
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUserDbContext, UserDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            var option = new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var dbContext = new UserDbContext(option))
            {
                dbContext.Database.EnsureCreated();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials());
            app.UseMvc();
        }
    }
}
