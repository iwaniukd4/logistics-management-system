namespace Logistics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder =
                WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            //AddSpaStaticFiles method is called to configure the app to serve static files 
            // for the Single-Page Application (SPA), which is the Vue.js application
            builder.Services.AddSpaStaticFiles(
            //The RootPath setting determines the root folder where the static files are located in the project
                configuration =>
            {
#if DEBUG
                configuration.RootPath = "ClientApp";
#else
                configuration.RootPath =
                    "ClientApp/dist/spa";
#endif
            });
            //the AddCors method is used to configure  Cross-Origin Resource Sharing (CORS) services for the application. 
            // CORS allows the server to specify which domains are permitted to access resources.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowLocalDevelopmentOrigin",
                    policy => policy
                        .WithOrigins(
                            "http://localhost:8080",
                            "http://localhost:5173",
                            "http://127.0.0.1:5173"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            var app = builder.Build();
            // UseStaticFiles method enables the application to serve static files (such as images, CSS, and JavaScript) 
            // from the wwwroot directory by default
            app.UseStaticFiles();
            //UseSpaStaticFiles method allows serving static files specifically configured for the SPA.
            app.UseSpaStaticFiles();
            //app.UseSpa() method configures the app to use an SPA framework. It sets the source path for the SPA to "ClientApp", which is where the Vue.js application is located.
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });
            app.UseCors("AllowLocalDevelopmentOrigin");
            app.MapControllers();
            app.Run();
        }
    }
}