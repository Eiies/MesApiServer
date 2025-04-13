using System.ComponentModel.DataAnnotations;

namespace MesApiServer.Data.Entities;
public class TrackInLog {
    [Key]
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public string ProductCode { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Operator { get; set; }
}

