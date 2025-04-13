namespace MesApiServer.Models;
public class ProcessEndRequest {
    public required string DeviceId { get; set; }
    public required string Result { get; set; }
    public DateTime EndTime { get; set; }

    public required string Operator { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
