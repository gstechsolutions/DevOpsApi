using DevOpsApi.core.api.ConfigurationModel;
using DevOpsApi.core.api.Data;
using DevOpsApi.core.api.Services.POSTempus;
using Microsoft.EntityFrameworkCore;
using NodaTime;

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


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            //.AddXmlSerializerFormatters()
            //.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.Converters.Add(new StringEnumConverter());
            //    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //    options.SerializerSettings.Formatting = Formatting.Indented;
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});

            var appSettingsSection = Configuration.GetSection("ServiceCore");
            var appSettings = appSettingsSection.Get<ServiceCoreSettings>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Accounting Core", Version = "v1" });
                c.EnableAnnotations();
            });

            services.AddLogging();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, STRDMSContext context)
        {
            app.UseCors("CorsPolicy");

            // Uncomment the below line to enable authentication
            app.UseAuthentication();



            app.UseMvc();


            //Uncommment for EF data migrations. Need to inject context to this method first.
            //context.Database.Migrate();

            app.UseRouting();
            app.UseAuthorization();
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
