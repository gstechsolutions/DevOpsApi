using DevOpsApi.core.api.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevOpsApi.core.api.Data
{
    public class STRDMSContext : DbContext
    {
        public STRDMSContext(DbContextOptions options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.Development.json")
                   .Build();

                var connectionString = configuration.GetConnectionString("STRDMSDB");

                var builder = new SqlConnectionStringBuilder(connectionString)
                {
                    //Password = EncryptDecrypt.Decrypt(configuration["DBPASSWORD"])
                    Password = (configuration["DBPASSWORD"])
                };

                optionsBuilder.UseSqlServer(builder.ConnectionString);
            }
        }

        public DbSet<Location> Locations { get; set; }

        public DbSet<PosInvoice> POSInvoices { get; set; }

        public DbSet<SISPosInvoice> SISPOSInvoices { get; set; }

        public DbSet<POSConfiguration> POSConfigurations { get; set; }

        public DbSet<POSDeviceConfiguration> POSDeviceConfigurations { get; set; }

        public DbSet<POSDeviceConfigurationHostName> POSDeviceConfigurationHostNames { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<POSLoginDetail> POSLoginDetails { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<RolePolicy> RolePolicies { get; set; }

    }
}
