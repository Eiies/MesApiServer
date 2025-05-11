namespace ApiServer.Models;

public class AskforFeed01Request {
    public required string AskTime { get; set; }
    public required string AskUser { get; set; }
    public required string AskforFeed { get; set; }
    public required string AskCode { get; set; }
}

public class AskforFeed02Request {
    public required string AskTime { get; set; }
    public required string AskUser { get; set; }
    public required string AskforType { get; set; }
    public required string AskCode { get; set; }
}

public class EnterSafeRequest {
    public required string UpTime { get; set; }
    public required string User { get; set; }
    public required string SafeState { get; set; }
    public required string AskCode { get; set; }
}

public class BarcodeUpRequest {
    public required string AskTime { get; set; }
    public required string AskUser { get; set; }
    public required string PanelUp { get; set; }
}

/// <summary>
/// 
/// </summary>
/*public class AgvCallbackRequest {
    [Required]
    [StringLength(32)]
    [JsonPropertyName("reqCode")]
    public required string ReqCode { get; set; }

    [Required]
    [StringLength(20)]
    [JsonPropertyName("reqTime")]
    public required string ReqTime { get; set; }

    [JsonPropertyName("cooX")]
    public decimal? CooX { get; set; }

    [JsonPropertyName("cooY")]
    public decimal? CooY { get; set; }

    [Required]
    [StringLength(32)]
    [JsonPropertyName("currentPositionCode")]
    public required string CurrentPositionCode { get; set; }

    [StringLength(2000)]
    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [StringLength(16)]
    [JsonPropertyName("mapCode")]
    public string? MapCode { get; set; }

    [StringLength(32)]
    [JsonPropertyName("mapDataCode")]
    public string? MapDataCode { get; set; }

    [StringLength(32)]
    [JsonPropertyName("stgBinCode")]
    public string? StgBinCode { get; set; }

    [Required]
    [StringLength(16)]
    [JsonPropertyName("method")]
    public required string Method { get; set; }

    [StringLength(16)]
    [JsonPropertyName("podCode")]
    public string? PodCode { get; set; }

    [StringLength(4)]
    [JsonPropertyName("podDir")]
    public string? PodDir { get; set; }

    [StringLength(32)]
    [JsonPropertyName("materialLot")]
    public string? MaterialLot { get; set; }

    [Required]
    [StringLength(5)]
    [JsonPropertyName("robotCode")]
    public required string RobotCode { get; set; }

    [Required]
    [StringLength(64)]
    [JsonPropertyName("taskCode")]
    public required string TaskCode { get; set; }

    [StringLength(32)]
    [JsonPropertyName("wbCode")]
    public string? WbCode { get; set; }

    [StringLength(30)]
    [JsonPropertyName("ctnrCode")]
    public string? CtnrCode { get; set; }

    [StringLength(2)]
    [JsonPropertyName("ctnrType")]
    public string? CtnrType { get; set; }

    [StringLength(16)]
    [JsonPropertyName("roadWayCode")]
    public string? RoadWayCode { get; set; }

    [StringLength(2)]
    [JsonPropertyName("seq")]
    public string? Seq { get; set; }

    [StringLength(32)]
    [JsonPropertyName("eqpCode")]
    public string? EqpCode { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
*/

/// <summary>
/// 
/// </summary>
/*public class AgvCallbackResponse {
    [JsonPropertyName("code")]
    public required string Code { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("reqCode")]
    public required string ReqCode { get; set; }
}*/

