﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly object loggerFactory;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


           



            // runs an error page
            app.UseExceptionHandler("/error.html");


            // remove before deployment for security purposes 
            var configuration = new ConfigurationBuilder()
                                    .AddEnvironmentVariables()
                                    .AddJsonFile(env.ContentRootPath + "/config.json")
                                    .Build();
            // used to add debug error page in testing phase.                                      
                                    



            if (configuration.GetValue<bool>("EnableDeveloperException"))
            {
                app.UseDeveloperExceptionPage();
            }



            // tells it to use the error page, 
            // To get it to work go to project -> properties -> Debug -> change the value for "ASPNETCORE_ENVIRONMENT"
            app.Use(async (context, next) =>
            {

                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("Error!");

                await next();


            });

            


            app.UseFileServer();
        }
    }
}
