namespace MesApiServer.Data.Entities {
    public class EQPConfirmLog {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string Barcode { get; set; }
        public DateTime ScanTime { get; set; }
    }
}
