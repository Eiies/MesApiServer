using MesApiServer.Services;
using Microsoft.OpenApi.Models;

namespace MesApiServer;

public class Startup(IConfiguration configuration){
    public IConfiguration Configuration{ get; } = configuration;
    public void ConfigureServices(IServiceCollection services){
        services.AddControllers();
        services.AddHttpClient<MesService>();

        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo{
                Title = "MES API",
                Version = "v1"
            });
        });
        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
        if (env.IsDevelopment()){
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MES API v1");
                c.RoutePrefix = "docs";
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}