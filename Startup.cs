using MesApiServer.Adapters;using MesApiServer.Repositories;using MesApiServer.Services;using Microsoft.OpenApi.Models;using Serilog;using Serilog.Settings.Configuration;namespace MesApiServer;public class Startup(IConfiguration configuration) {    private IConfiguration Configuration { get; } = configuration;    public void ConfigureServices(IServiceCollection services) {
        // 注册数据库上下文（扩展方法已封装注册逻辑）
        services.AddMySqlDatabase(Configuration);

        // 注册控制器
        services.AddControllers();

        services.AddScoped<IMesAdapter, MesAdapter>();


        // 注册业务服务和仓储
        services.AddScoped<IDeviceService, DeviceService>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();

        // 注册 Swagger 生成器
        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo {
                Title = "MES API",
                Version = "v1"
            });
        });

        // 鉴权
        // services.AddAuthentication(Configuration);
    }    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {        if(env.IsDevelopment()) {            app.UseDeveloperExceptionPage();            app.UseSwagger();            app.UseSwaggerUI(c => {                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MES API v1");                c.RoutePrefix = "docs";            });        }

        //// 初始化 Serilog
       Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(Configuration)
           .Enrich.FromLogContext()
           .CreateLogger();


        // 全局异常捕获并记录
        app.Use(async (context, next) => {
            try {
                await next();
            } catch(Exception ex) {
                Log.Error(ex, "Unhandled exception");
                throw;
            }
        });
        app.UseHttpsRedirection();        app.UseRouting();

        // 如果未来启用鉴权，则取消注释下面两行
        // app.UseAuthentication();
        // app.UseAuthorization();

        app.UseAuthorization();        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });    }}