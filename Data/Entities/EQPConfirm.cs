using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesApiServer.Data.Entities;
public class EQPConfirm {
    [Key]
    public int Id { get; set; }
    public string EQPID { get; set; } = null!;
    [ForeignKey("DeviceId")]
    public Device? Device { get; set; }

    public string Barcode { get; set; } = null!;
    public DateTime ScanTime { get; set; }
    public string Operator { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
