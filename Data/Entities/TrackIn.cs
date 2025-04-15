using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesApiServer.Data.Entities;
public class TrackIn {
    [Key]
    public int Id { get; set; }
    public string EQPID { get; set; } = null!;
    public string LotID { get; set; } = null!;

    [ForeignKey("DeviceId")]
    public Device? Device { get; set; }
    public string CarrierID { get; set; } = null!;
    public string EmployeeID { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

