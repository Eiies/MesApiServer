// ProcessEndRequest.cs
namespace MesApiServer.Models;
public class ProcessEndRequest {
    public string DeviceId { get; set; }
    public string Result { get; set; }
    public DateTime EndTime { get; set; }
}
