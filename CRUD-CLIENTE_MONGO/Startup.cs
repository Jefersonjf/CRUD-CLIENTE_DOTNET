using CRUD_CLIENTE_MONGO.Context;
using CRUD_CLIENTE_MONGO.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CRUD_CLIENTE_MONGO
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            // ADICIONA A REFERENCIA DAS IMPLEMENTAÇÕES DAS INTERFACES 
            services.AddScoped<IClienteContext, ClienteContext>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD_CLIENTE_MONGO", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD_CLIENTE_MONGO v1"));
           
            app.UseCors(builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
