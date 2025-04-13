namespace MesApiServer.Models;
public class EQP2DConfirmRequest {
    public required string DeviceId { get; set; }
    public required string Barcode { get; set; }
    public DateTime ScanTime { get; set; }
}
