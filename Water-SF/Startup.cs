using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Water_SF.Data;
using Water_SF.Services;

namespace Water_SF
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
            services.AddTransient<ITareasService, TareaService>();
            services.AddDbContext<TareaContext>(options => options.UseInMemoryDatabase("tareadb"));

            services.AddTransient<IProveedoresService, ProveedorService>();
            services.AddDbContext<ProveedorContext>(options => options.UseInMemoryDatabase("proveedordb"));

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhostFrontend", builder =>
                {
                    builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Water-SF",
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Web API for Water-SF"
                });
            });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TareaService API v1");
                });
            }

            // 🧩 Habilitar CORS antes de UseRouting
            app.UseCors("AllowLocalhostFrontend");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
