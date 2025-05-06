using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiServer.Data.Entities {
    public class ProcessEnd {
        [Key] public int Id { get; set; }

        public string DeviceId { get; set; } = string.Empty;

        [ForeignKey("DeviceId")] public Device Device { get; set; } = default!;

        public string CarrierID { get; set; } = string.Empty; // <载具ID>

        public string LotID { get; set; } = string.Empty; // <批次号>

        public DateTime EndTime { get; set; } = default;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}