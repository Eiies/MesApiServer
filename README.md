# MesApiServer

```txt
MesApiServer/
├── Controllers/
│   └── EquipmentController.cs		// 接口控制器层，仅负责解析请求、调用业务服务
├── Data/
│   ├── AppDbContext.cs              // EF Core DbContext
│   └── Entities/
│       └── Device.cs                // 数据库实体（例如 Device）
├── Models/
│   ├── AliveCheckRequest.cs         // DTO（数据传输对象），不参与 EF 数据库映射
│   └── ...							// 其他API请求/响应相关模型
├── Repositories/
│   ├── IDeviceRepository.cs         // 数据访问接口
│   └── DeviceRepository.cs          // 数据访问实现
├── Adapters/
│   └── IMesAdapter.cs               // MES系统接口适配器接口及其实现（例如 
MesAdapter）
├── Services/
│   ├── IDeviceService.cs            // 业务服务接口
│   └── DeviceService.cs             // 业务服务实现
├── Program.cs
├── Startup.cs
└── appsettings.json
```

**dotnet 生成开发证书**

```bash
创建证书
dotnet dev-certs https -ep ./https.pfx -p 123456 --trust

Linux 信任证书
sudo cp https.pfx /usr/local/share/ca-certificates/https.crt
sudo update-ca-certificates
```

**OpenSSL 生成自签证书**
```创建 openssl.cnf 文件```

```
[req]
default_bits       = 2048
prompt             = no
default_md         = sha256
req_extensions     = req_ext
distinguished_name = dn

[dn]
CN = 192.168.1.63

[req_ext]
subjectAltName = @alt_names

[alt_names]
IP.1 = 192.168.1.63
```

**使用配置生成证书**

```bash
# 生成私钥
openssl genrsa -out cert.key 2048

# 生成证书（有效期 666 天）
openssl req -x509 -new -nodes -key cert.key -sha256 -days 666 -out cert.crt -config openssl.cnf

合并为 PFX
openssl pkcs12 -export -out https.pfx -inkey cert.key -in cert.crt -password pass:123456
```

```bash
Add-Migration InitialCreate -o Data/Migrations

Update-Database
```

**执行数据库迁移**

```bash
dotnet ef migrations add InitialCreate -o Data/Migrations
```

**更新数据库**

```bash
dotnet ef database update
```
