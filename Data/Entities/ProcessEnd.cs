using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesApiServer.Data.Entities;
public class ProcessEnd {
    [Key]
    public int Id { get; set; }
    public string EQPID { get; set; } = null!;

    [ForeignKey("DeviceId")]
    public Device? Device { get; set; }
    public string Result { get; set; } = null!;
    public DateTime EndTime { get; set; }

    public string Operator { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

