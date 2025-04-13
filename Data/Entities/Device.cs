using MesApiServer.Models;

namespace MesApiServer.Data.Entities;

public class Device {
    public int Id { get; set; }
    /// <summary>
    /// 设备标识（经过标准化处理）
    /// </summary>
    public required string DeviceId { get; set; }
    /// <summary>
    /// 最近一次心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }
}
