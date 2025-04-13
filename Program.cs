using MesApiServer.Adapters;
using MesApiServer.Data;
using MesApiServer.Repositories;
using MesApiServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==================== 日志配置 ====================
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// ==================== 数据库配置 ====================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("缺少数据库连接字符串配置");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mysql => {
            mysql.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// ==================== 认证配置 ====================
var jwtSettings = builder.Configuration.GetSection("Jwt");
if(jwtSettings.Exists()) {
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

builder.Services.AddScoped<IMesAdapter, MesAdapter>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

// ==================== 应用构建 ====================
var app = builder.Build();

// ==================== 数据库迁移 ====================
using(var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try {
        db.Database.Migrate();
        Log.Information("数据库架构已更新");
    } catch(Exception ex) {
        Log.Fatal(ex, "数据库迁移失败");
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

app.UseHttpsRedirection();
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