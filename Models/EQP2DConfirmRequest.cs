namespace MesApiServer.Models;
public class EQP2DConfirmRequest {
    public string DeviceId { get; set; }
    public string Barcode { get; set; }
    public DateTime ScanTime { get; set; }
}
