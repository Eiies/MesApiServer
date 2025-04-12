namespace MesApiServer.Models;
public class TrackInRequest {
    public string DeviceId { get; set; }
    public string ProductCode { get; set; }
    public DateTime StartTime { get; set; }
}
