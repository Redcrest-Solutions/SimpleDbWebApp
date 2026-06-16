/***************************************************************************
 * View => Other Windows => Package Manager Console
 * Add-Migration <Name>
***************************************************************************/

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SimpleDbWebApp.Models
{
    public class SimpleContext : DbContext
    {
        private string connectionString = @"server=localhost;database=simpledb;user=root;password=;";
        public static MySqlServerVersion ServerVersion = new(new Version(5, 7, 0)); // Must use this version to ensure rename commands are correctly generated

        public DbSet<User> Users { get; set; }

        // Used by the migration tool
        public SimpleContext()
        {
        }

        // Constructor used while requesting an instance from the main program class
        public SimpleContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Default constructor used by the injection
        public SimpleContext(DbContextOptions<SimpleContext> options) : base(options)
        {
        }

        /// <summary>
        /// Defines the keys of the tables
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            //optionsBuilder.UseMySql("connection string", options => options.MaxBatchSize(100));
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseMySql(connectionString, ServerVersion, options =>
                    {
                        options.MaxBatchSize(100);
                    });
            }
            optionsBuilder.ConfigureWarnings(delegate (WarningsConfigurationBuilder warnings)
            {
                // The following line will suppress the warning
                // "'Foo.Bar' and 'Bar.Foo' were separated into two relationships as
                // ForeignKeyAttribute was specified on properties 'BarId' and
                // 'FooId' on both sides."
                warnings.Ignore(CoreEventId.ForeignKeyAttributesOnBothNavigationsWarning);
            });
        }

        public void CreateAndMigrate()
        {
            Database.Migrate();
        }
    }
}
