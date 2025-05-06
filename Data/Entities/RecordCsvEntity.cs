using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiServer.Data.Entities;

public class RecordCsvEntity {
    [Key]
    public int Id { get; set; }
    public required string QRCode { get; set; }
    public required string EngravingContent { get; set; }
    public required string CategoryResult { get; set; }
    public required string ANgPoints { get; set; }
    public required string BNgPoints { get; set; }
    public required string Group1 { get; set; }
    public required string Group2 { get; set; }
    public required string Group3 { get; set; }

    public List<RecordCsvValue> Values { get; set; } = [];
}

public class RecordCsvValue {
    [Key]
    public int Id { get; set; }
    public int Index { get; set; }

    [Column(TypeName = "decimal(10,3)")]
    public decimal Value { get; set; }

    public int RecordCsvEntityId { get; set; }
    public RecordCsvEntity RecordCsvEntity { get; set; } = null!;
}

