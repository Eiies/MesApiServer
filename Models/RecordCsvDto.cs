using System.Text.Json;
using System.Text.Json.Serialization;
using JsonExtensionDataAttribute = System.Text.Json.Serialization.JsonExtensionDataAttribute;

namespace ApiServer.Models;

public class RecordCsvDto {
    [JsonPropertyName("二维码")]
    public required string QRCode { get; set; }

    [JsonPropertyName("刻字内容")]
    public required string EngravingContent { get; set; }

    [JsonPropertyName("分类结果")]
    public required string CategoryResult { get; set; }

    [JsonPropertyName("A类NG点")]
    public required string ANgPoints { get; set; }

    [JsonPropertyName("B类NG点")]
    public required string BNgPoints { get; set; }

    [JsonPropertyName("组合1")]
    public required string Group1 { get; set; }

    [JsonPropertyName("组合2")]
    public required string Group2 { get; set; }

    [JsonPropertyName("组合3")]
    public required string Group3 { get; set; }

    // 动态字段：1 到 16
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Values { get; set; }
}
