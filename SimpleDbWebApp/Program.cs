using SimpleDbWebApp.Models;

namespace SimpleDbWebApp
{
    public class Program
    {
        public static string ConnectionString { get; private set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            ConnectionString = builder.Configuration.GetConnectionString("DbConnection");

            builder.Services.AddDbContext<Models.SimpleContext>(c =>
            {
                c.UseMySql(ConnectionString, Models.SimpleContext.ServerVersion);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            UpdateDatabase();

            app.Run();
        }

        public static SimpleContext GetDbContext()
        {
            return new Models.SimpleContext(ConnectionString);
        }
        private static void UpdateDatabase()
        {
            using var context = Program.GetDbContext();
            context.CreateAndMigrate();
        }
    }
}
