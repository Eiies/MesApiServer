using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiServer.Data.Entities {
    public class TrackIn {
        [Key] public int Id { get; set; }

        // 这里直接引用 DeviceId（string）与 Device 实体关联
        public string DeviceId { get; set; } = string.Empty;

        [ForeignKey("DeviceId")] public Device Device { get; set; } = default!;

        public string LotID { get; set; } = string.Empty; // <批次号>

        public string CarrierID { get; set; } = string.Empty; // <载具ID>

        public string EmployeeID { get; set; } = string.Empty; // <员工ID>  

        public DateTime TrackTime { get; set; } = DateTime.Now; // <上机时间>

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}