using MesApiServer.Data;
using Microsoft.EntityFrameworkCore;

namespace MesApiServer.Services;
public static class ServiceExtensions {
    /// <summary>
    /// 通过扩展方法注册 MySQL 数据库上下文
    /// </summary>
    /// <param name="services">IServiceCollection 实例</param>
    /// <param name="configuration">IConfiguration 实例</param>
    public static IServiceCollection AddMySqlDatabase(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString,
                new MySqlServerVersion(new Version(8, 0, 21))));
        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration) {
        // 添加身份验证的配置
        //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //         .AddJwtBearer(options => {
        //             options.TokenValidationParameters = new TokenValidationParameters {
        //                 ValidateIssuer = true,
        //                 ValidateAudience = true,
        //                 ValidateLifetime = true,
        //                 ValidateIssuerSigningKey = true,
        //                 ValidIssuer = configuration["Jwt:Issuer"],
        //                 ValidAudience = configuration["Jwt:Audience"],
        //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        //             };
        //         });
        return services;
    }
}
