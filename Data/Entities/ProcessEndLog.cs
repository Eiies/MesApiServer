namespace MesApiServer.Data.Entities {
    public class ProcessEndLog {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string Result { get; set; }
        public DateTime EndTime { get; set; }
    }
}
