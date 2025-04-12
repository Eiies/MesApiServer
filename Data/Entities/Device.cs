namespace MesApiServer.Data.Entities;

public class Device {
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public DateTime LastHeartbeat { get; set; }
}
