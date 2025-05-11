using System.ComponentModel.DataAnnotations;

namespace ApiServer.Data.Entities;
public class RcsEntity {
    [Key]
    public int Id { get; set; }

    public string? ReqCode { get; set; }

    public required string ReqTime { get; set; }

    public string? CooX { get; set; }

    public string? CooY { get; set; }

    public required string CurrentPositionCode { get; set; }

    public string? MapCode { get; set; }

    public string? MapDataCode { get; set; }

    public required string Method { get; set; } // "start", "end", 

    public string? PodCode { get; set; }

    public string? PodDir { get; set; }

    public required string RobotCode { get; set; } // AGV编号

    public required string TaskCode { get; set; }

    public string? WbCode { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;
}

