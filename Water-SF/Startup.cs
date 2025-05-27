using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Water_SF.Data;
using Water_SF.Services;
using Water_SF.Helpers;
using Water_SF.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
            // 1. Configurar JwtSettings desde appsettings.json
            var jwtSettings = Configuration
                                .GetSection("JwtSettings")
                                .Get<JwtSettings>()
                                ?? throw new InvalidOperationException("Invalid JWT Settings");

            services.AddSingleton(jwtSettings);
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // 2. Configurar Autenticación JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // 3. CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhostFrontend", builder =>
                {
                    builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            // 4. Autorización
            services.AddAuthorization();

            // 5. Otros servicios propios
            services.AddTransient<ITareasService, TareaService>();
            services.AddDbContext<TareaContext>(options => options.UseInMemoryDatabase("tareadb"));

            services.AddTransient<IProveedoresService, ProveedorService>();
            services.AddDbContext<ProveedorContext>(options => options.UseInMemoryDatabase("proveedordb"));

            services.AddTransient<IInventarioService, InventarioService>();
            services.AddDbContext<InventarioContext>(options => options.UseInMemoryDatabase("inventariodb"));

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // 6. Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Water-SF",
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Web API for Water-SF"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Water-SF API v1");
                });
            }

            app.UseCors("AllowLocalhostFrontend");
            app.UseRouting();

            // Agregar autenticación y autorización al pipeline
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
