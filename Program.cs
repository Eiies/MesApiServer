using ApiServer.Adapters;
using ApiServer.Data;
using ApiServer.Repositories;
using ApiServer.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Text;

/*[DllImport("kernel32.dll")]
static extern bool FreeConsole();
FreeConsole();*/

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// ==================== CORS 配置 ====================
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// ==================== 自签证书 ====================
string certPath = Path.Combine(AppContext.BaseDirectory, "Cert", "root.pfx");

builder.WebHost.ConfigureKestrel(opt => {
    opt.ListenAnyIP(5001, listenOptions => {
        listenOptions.UseHttps(certPath, "123456");
    });
    opt.ListenAnyIP(5000);
});

// 加载 .env 文件
Env.Load();

// ==================== 日志配置 ====================
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: 7,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

// ==================== 数据库配置 ====================
string connectionString =
    $"Server={Env.GetString("MYSQL_HOST")};Port={Env.GetString("MYSQL_PORT")};Database={Env.GetString("MYSQL_DATABASE")};User={Env.GetString("MYSQL_USER")};Password={Env.GetString("MYSQL_PASSWORD")};";

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mysql => {
            mysql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
        }));

// ==================== 认证配置 ====================
IConfigurationSection jwtSettings = builder.Configuration.GetSection("Jwt");
if(jwtSettings.GetChildren().Any()) {
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["Key"]!)),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
}

// ==================== 服务注册 ====================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IMesAdapter, MqttMesAdapter>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IRecordService, RecordCsvService>();

// ==================== 应用构建 ====================
WebApplication app = builder.Build();

// ==================== 数据库迁移 ====================
using(IServiceScope scope = app.Services.CreateScope()) {
    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try {
        db.Database.Migrate();
        Log.Information("数据库架构已更新");
    } catch(Exception ex) {
        Log.Fatal(ex, "数据库迁移失败：未知异常");
        throw;
    }
}

// ==================== 中间件管道 ====================
if(app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();

    // Scalar API
    app.MapOpenApi();
    app.MapScalarApiReference();
} else {
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseCors();
//app.UseHttpsRedirection();
app.UseRouting();

if(jwtSettings.Exists()) {
    // TODO: 需要添加身份验证中间件，暂不启用

    //app.UseAuthentication();
    //app.UseAuthorization();
}

app.MapControllers();

// ==================== 错误处理端点 ====================
app.MapGet("/error", () => Results.Problem(
    statusCode: StatusCodes.Status500InternalServerError,
    title: "服务器内部错误"));

// ==================== 启动应用 ====================
try {
    Log.Information("应用程序启动中...");
    app.MapGet("/", () => Results.Ok("测试通过"));
    app.Run();
} catch(Exception ex) {
    Log.Fatal(ex, "应用程序启动失败");
} finally {
    Log.CloseAndFlush();
}