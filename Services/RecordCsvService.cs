using ApiServer.Data;
using ApiServer.Data.Entities;
using ApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Services;

public interface IRecordService {
    Task SaveRecordAsync(RecordCsvDto dto);
}

public class RecordCsvService(AppDbContext context) :IRecordService {
    public async Task SaveRecordAsync(RecordCsvDto dto) {
        var e = await context.RecordCsvEntities
            .Include(r => r.Values)
            .FirstOrDefaultAsync(r => r.QRCode == dto.QRCode);

        if(e != null) {
            // 更新已存在的记录
            e.EngravingContent = dto.EngravingContent;
            e.CategoryResult = dto.CategoryResult;
            e.ANgPoints = dto.ANgPoints;
            e.BNgPoints = dto.BNgPoints;
            e.Group1 = dto.Group1;
            e.Group2 = dto.Group2;
            e.Group3 = dto.Group3;

            // 清除旧值
            context.RecordCsvValues.RemoveRange(e.Values);
            e.Values.Clear();

            // 添加新值
            for(int i = 1;i <= 16;i++) {
                if(dto.Values != null && dto.Values.TryGetValue(i.ToString(), out var token) &&
                    decimal.TryParse(token.ToString(), out var value)) {
                    e.Values.Add(new RecordCsvValue {
                        Index = i,
                        Value = value
                    });
                }
            }
        } else {
            // 新建记录
            var record = new RecordCsvEntity {
                QRCode = dto.QRCode,
                EngravingContent = dto.EngravingContent,
                CategoryResult = dto.CategoryResult,
                ANgPoints = dto.ANgPoints,
                BNgPoints = dto.BNgPoints,
                Group1 = dto.Group1,
                Group2 = dto.Group2,
                Group3 = dto.Group3,
                Values = []
            };

            for(int i = 1;i <= 16;i++) {
                if(dto.Values != null && dto.Values.TryGetValue(i.ToString(), out var token) &&
                    decimal.TryParse(token.ToString(), out var value)) {
                    record.Values.Add(new RecordCsvValue {
                        Index = i,
                        Value = value
                    });
                }
            }

            context.RecordCsvEntities.Add(record);
        }

        await context.SaveChangesAsync();
    }
}

