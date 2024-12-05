using DevOpsApi.core.api.ConfigurationModel;
using DevOpsApi.core.api.Data;
using DevOpsApi.core.api.Services.Auth;
using DevOpsApi.core.api.Services.POSTempus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using System.Text;

namespace DevOpsApi.core.api
{
    public class Startup
    {
        readonly IConfiguration Configuration;

        public Startup(IWebHostEnvironment hostingEnv)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(hostingEnv.ContentRootPath)
                .AddJsonFile($"appsettings.{hostingEnv.EnvironmentName}.json", true)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Third party libraries
            services.AddSingleton<IClock>(SystemClock.Instance);
            services.AddAutoMapper(typeof(MappingProfile));

            //EF DB Context
            services.AddDbContext<STRDMSContext>(options => options.UseSqlServer(BuildConnectionString()));

            //General configurations
            services.Configure<ServiceCoreSettings>(Configuration.GetSection("ServiceCore"));

            //DI Services           
            services.AddScoped<ITempusService, TempusService>();
            services.AddScoped<IJwtService, JwtService>();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);


            var appSettingsSection = Configuration.GetSection("ServiceCore");
            var appSettings = appSettingsSection.Get<ServiceCoreSettings>();


            // Load JWT settings from configuration
            var jwtSettings = Configuration.GetSection("JwtConfig");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });

            services.AddAuthorization();

            // Add Swagger with JWT support
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Accounting Core", Version = "v1" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });


            services.AddLogging();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, STRDMSContext context)
        {
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            // Configure the middleware order for JWT and routing
            app.UseRouting();

            // Uncomment the below line to enable authentication
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Accounting Core V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();


            //Uncommment for EF data migrations. Need to inject context to this method first.
            context.Database.Migrate();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

        private string BuildConnectionString()
        {
            var dbConn = Configuration.GetConnectionString("STRDMS");
            var dbServer = Configuration["DBSERVER"];
            var dbName = Configuration["DBNAME"];
            var dbUser = Configuration["DBUSER"];
            //var dbPwd = EncryptDecrypt.Decrypt(Configuration["DBPASSWORD"]);
            var dbPwd = (Configuration["DBPASSWORD"]);

            return string.Format(dbConn, dbName, dbServer, dbUser, dbPwd);
        }
    }
}
