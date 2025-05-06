using System.ComponentModel.DataAnnotations;

namespace ApiServer.Data.Entities {
    // 使用 DeviceId 作为主键，确保唯一性
    public class Device {
        [Key] public string DeviceId { get; set; } = string.Empty;

        public DateTime LastHeartbeat { get; set; } = default;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 导航属性，方便查询设备相关的记录
        public ICollection<TrackIn> TrackIns { get; set; } = new List<TrackIn>();
        public ICollection<EQPConfirm> EQPConfirms { get; set; } = new List<EQPConfirm>();
        public ICollection<ProcessEnd> ProcessEnds { get; set; } = new List<ProcessEnd>();
    }
}