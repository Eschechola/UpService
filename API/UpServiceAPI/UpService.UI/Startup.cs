using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using UpService.UI.StatusCode;
using UpServiceAPI.Application.Services;
using UpServiceAPI.Infra.DAO;
using UpServiceAPI.Infra.DTO;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;
using UpServiceAPI.Infra.Repository;

namespace UpService.UI
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
            #region WEB DI

            services.AddSingleton(Configuration);

            services.AddSingleton<IStatusCodeActionResult>(new Error500().StatusCode( 500, new { message = "Ocorreu um erro interno na aplicação por favor tente novamente" }));

            #endregion


            #region Services DI

            services.AddTransient<IClientService, ClientService>();

            services.AddTransient<IJobService, JobService>();

            #endregion


            #region Repository DI

            services.AddTransient<IClientRepository, ClientRepository>();

            services.AddTransient<IJobRepository, JobRepository>();

            #endregion


            #region Infra DI

            services.AddTransient<IMySqlConnection, DBConnection>();

            #endregion


            #region AutoMapper Config

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientDTO, Client>();
                cfg.CreateMap<Client, ClientDTO>();

                cfg.CreateMap<JobDTO, Job>();
                cfg.CreateMap<Job, JobDTO>();
            });

            services.AddSingleton(autoMapperConfig.CreateMapper());

            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Description = "UpService API v1",
                    Title = "UpService",
                    Version = "v1",
                    Contact = new OpenApiContact { },
                });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "UpService API v1.0");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
