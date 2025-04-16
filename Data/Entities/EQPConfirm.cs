using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesApiServer.Data.Entities;

public class EQPConfirm {
    [Key]
    public int Id { get; set; }

    public string DeviceId { get; set; } = string.Empty;

    [ForeignKey("DeviceId")]
    public Device Device { get; set; } = default!;

    public DateTime ScanTime { get; set; } = default; // <扫码时间>

    public string LotID { get; set; } = string.Empty; // <批次号>

    public string EQP2DID { get; set; } = string.Empty; // <二维码ID>

    public string Orientation { get; set; } = string.Empty; // <方向>

    public string RotationAngle { get; set; } = string.Empty; // <旋转角度>

    public string ConnectMode { get; set; } = string.Empty; // <连接方式>

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

