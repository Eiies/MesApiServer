using System.ComponentModel.DataAnnotations;

namespace MesApiServer.Data.Entities;

public class Device {
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 设备标识（经过标准化处理）
    /// </summary>
    public string DeviceId { get; set; } = null!;
    /// <summary>
    /// 最近一次心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

