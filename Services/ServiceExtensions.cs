using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MesApiServer.Data;

namespace MesApiServer.Services
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 通过扩展方法注册 MySQL 数据库上下文
        /// </summary>
        /// <param name="services">IServiceCollection 实例</param>
        /// <param name="configuration">IConfiguration 实例</param>
        public static IServiceCollection AddMySqlDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(8, 0, 21))));
            return services;
        }
    }
}