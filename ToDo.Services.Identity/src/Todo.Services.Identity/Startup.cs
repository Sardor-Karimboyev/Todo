using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Todo.Services.Identity.Models.Entities;
using ToDo.Common;
using ToDo.Common.Authentication;
using ToDo.Common.Dispatchers;
using ToDo.Common.Mongo;
using ToDo.Common.Mvc;
using ToDo.Common.Swagger;

namespace Todo.Services.Identity
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };
        public IContainer Container { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddSwaggerDocs();
            services.AddJwt();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.AddControllers();


            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddMongo();
            builder.AddMongoRepository<RefreshToken>("RefreshTokens");
            builder.AddMongoRepository<User>("Users");
            builder.AddDispatchers();
            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAccessTokenValidator();
            app.UseServiceId();

            startupInitializer.InitializeAsync();
        }
        //public void Configure(IApplicationBuilder app, 
        //    IStartupInitializer startupInitializer)
        //{
        //    app.UseAllForwardedHeaders();
        //    app.UseSwaggerDocs();
        //    app.UseErrorHandler();
        //    app.UseAuthentication();
        //    app.UseAccessTokenValidator();
        //    app.UseServiceId();
        //   // app.UseMvc();



        //    startupInitializer.InitializeAsync();
        //}
    }
}
