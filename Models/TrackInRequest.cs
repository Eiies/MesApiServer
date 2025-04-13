namespace MesApiServer.Models;

public class TrackInRequest {
    public required string DeviceId { get; set; }
    public required string ProductCode { get; set; }
    public DateTime StartTime { get; set; }

    public string Operator { get; set; }
}