namespace MesApiServer.Models;

public class DeviceDto {
    public int Id { get; set; }
    public required string DeviceId { get; set; }
    public DateTime Timestamp { get; set; }
    public required string DataPayload { get; set; }
}

