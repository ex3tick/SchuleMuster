namespace WebApp
{
    /// <summary>
    /// The main program class for the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">An array of arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure the application as an MVC application
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "SessionCookie";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = false;
            });

            var app = builder.Build();

            // Uncomment to map a default GET request to return "Hello World!"
            // app.MapGet("/", () => "Hello World!");

            // Enable serving static files
            app.UseStaticFiles();

            // Configure routing
            app.UseRouting();

            // Enable session management
            app.UseSession();

            // Set up default controller route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Run the application
            app.Run();
        }
    }
}