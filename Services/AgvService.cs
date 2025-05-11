using ApiServer.Data;
using ApiServer.Data.Entities;
using ApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Services;

public interface IAgvService {
    Task<bool> SaveRequestAsync(AgvCallbackRequest request);
    Task<RcsEntity?> GetRecordAsync(string reqCode);
    AgvCallbackResponse HandleAgvCallback(AgvCallbackRequest req);
}

public class AgvService(AppDbContext context, ILogger<AgvService> logger) :IAgvService {
    public AgvCallbackResponse HandleAgvCallback(AgvCallbackRequest req) {

        // 使用 SaveRequestAsync
        if(SaveRequestAsync(req).Result) {
            return new AgvCallbackResponse {
                Code = "0",
                Message = "Success",
                ReqCode = req.ReqCode
            };
        }

        return new AgvCallbackResponse {
            Code = "1",
            Message = "Failure",
            ReqCode = req.ReqCode
        };
    }
    public async Task<bool> SaveRequestAsync(AgvCallbackRequest req) {
        try {
            var record = new RcsEntity {
                ReqCode = req.ReqCode,
                ReqTime = req.ReqTime,
                RobotCode = req.RobotCode,
                TaskCode = req.TaskCode,
                Method = req.Method,
                CooX = req.CooX,
                CooY = req.CooY,
                WbCode = req.WbCode,
                MapCode = req.MapCode,
                MapDataCode = req.MapDataCode,
                PodCode = req.PodCode,
                PodDir = req.PodDir,
                CurrentPositionCode = req.CurrentPositionCode,
            };

            await context.RcsEntities.AddAsync(record);
            await context.SaveChangesAsync();

            logger.LogInformation($"Saved AGV request: {req.ReqCode}");
            return true;
        } catch(DbUpdateException ex) {
            logger.LogError(ex, $"Database save failed for {req.ReqCode}");
            return false;
        } catch(Exception ex) {
            logger.LogError(ex, $"Error saving AGV request: {req.ReqCode}");
            return false;
        }
    }
    public async Task<RcsEntity?> GetRecordAsync(string reqCode) {
        return await context.RcsEntities.FirstOrDefaultAsync(r => r.ReqCode == reqCode);
    }
}

