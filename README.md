# MesApiServer

``txt
MesApiServer/
├── Controllers/
│   └── EquipmentController.cs     // 接口控制器层，仅负责解析请求、调用业务服务
├── Data/
│   ├── AppDbContext.cs              // EF Core DbContext
│   └── Entities/
│       └── Device.cs                // 数据库实体（例如 Device）
├── Models/
│   ├── AliveCheckRequest.cs         // DTO（数据传输对象），不参与 EF 数据库映射
│   └── ...                        // 其他API请求/响应相关模型
├── Repositories/
│   ├── IDeviceRepository.cs         // 数据访问接口
│   └── DeviceRepository.cs          // 数据访问实现
├── Adapters/
│   └── IMesAdapter.cs               // MES系统接口适配器接口及其实现（例如 MesAdapter）
├── Services/
│   ├── IDeviceService.cs            // 业务服务接口
│   └── DeviceService.cs             // 业务服务实现
├── Program.cs
├── Startup.cs
└── appsettings.json
```


**执行数据库迁移** 
```bash
dotnet ef migrations add InitialCreate -o Data/Migrations
```

**更新数据库**
```bash
dotnet ef database update
```
