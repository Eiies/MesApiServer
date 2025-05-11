using System.Text.Json.Serialization;

namespace ApiServer.Models;

public class BaseRcsRequest {
    [JsonPropertyName("reqCode")]
    public string ReqCode { get; set; } = Guid.NewGuid().ToString("N");

    [JsonPropertyName("reqTime")]
    public string ReqTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    [JsonPropertyName("clientCode")]
    public string? ClientCode { get; set; }

    [JsonPropertyName("tokenCode")]
    public string? TokenCode { get; set; }
}

public class BaseRcsResponse {
    [JsonPropertyName("code")]
    public required string Code { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("reqCode")]
    public required string ReqCode { get; set; }
}

public class GenAgvSchedulingTaskRequest :BaseRcsRequest { // (client)

    [JsonPropertyName("taskTyp")]
    public string? TaskType { get; set; }

    [JsonPropertyName("wbCode")]
    public string? WbCode { get; set; }

    [JsonPropertyName("priority")]
    public string? Priority { get; set; } // 1-127

    [JsonPropertyName("positionCodePath")]
    public required List<PositionCodePathItem> PositionCodePath { get; set; }

    [JsonPropertyName("podCode")]
    public string? PodCode { get; set; }

    [JsonPropertyName("podDir")]
    public string? PodDir { get; set; }

    [JsonPropertyName("podTyp")]
    public string? PodTyp { get; set; }

    [JsonPropertyName("taskCode")]
    public string? TaskCode { get; set; }

    [JsonPropertyName("agvCode")]
    public string? AgvCode { get; set; }
}

public class PositionCodePathItem {
    [JsonPropertyName("positionCode")]
    public required string PositionCode { get; set; }
    [JsonPropertyName("type")]
    public required string Type { get; set; } // "00", "02", "03"
}



public class AgvCallbackRequest { // (server)
    [JsonPropertyName("reqCode")]
    public string? ReqCode { get; set; }

    [JsonPropertyName("reqTime")]
    public required string ReqTime { get; set; }

    [JsonPropertyName("cooX")]
    public string? CooX { get; set; }

    [JsonPropertyName("cooY")]
    public string? CooY { get; set; }

    [JsonPropertyName("currentPositionCode")]
    public required string CurrentPositionCode { get; set; }

    [JsonPropertyName("mapCode")]
    public string? MapCode { get; set; }

    [JsonPropertyName("mapDataCode")]
    public string? MapDataCode { get; set; }

    [JsonPropertyName("method")]
    public required string Method { get; set; } // "start", "end", 
    [JsonPropertyName("podCode")]
    public string? PodCode { get; set; }

    [JsonPropertyName("podDir")]
    public string? PodDir { get; set; }

    [JsonPropertyName("robotCode")]
    public required string RobotCode { get; set; } // AGV编号

    [JsonPropertyName("taskCode")]
    public required string TaskCode { get; set; }

    [JsonPropertyName("wbCode")]
    public string? WbCode { get; set; }
}

public class AgvCallbackResponse {
    [JsonPropertyName("code")]
    public required string Code { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("reqCode")]
    public string? ReqCode { get; set; }
}