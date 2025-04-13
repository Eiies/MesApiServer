using System.ComponentModel.DataAnnotations;

namespace MesApiServer.Data.Entities;
    public class EQPConfirmLog {
        [Key]
        public int Id { get; set; }
        public required string DeviceId { get; set; }
        public required string Barcode { get; set; }
        public DateTime ScanTime { get; set; }

        public required string Operator { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
