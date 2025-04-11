using Serilog;
using Serilog.AspNetCore;

namespace MesApiServer;

internal static class Program {
    public static void Main(string[] args) {
        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(
               new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build())
           .Enrich.FromLogContext()
           .CreateLogger();

        try {
            Log.Information("Starting web host");
            CreateHostBuilder(args).Build().Run();
        } catch(Exception ex) {
            Log.Fatal(ex, "Host terminated unexpectedly");
        } finally {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) {
        return Host.CreateDefaultBuilder(args)
            .UseSerilog() // 替换内置日志
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
