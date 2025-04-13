namespace MesApiServer.Models;

public class AliveCheckRequest {
    public required string DeviceId { get; set; }
    public DateTime Timestamp { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
