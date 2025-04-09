namespace MesApiServer.Models;

public class WorkOrderDto{
    public required string WorkOrderId{ get; set; }
    public required string ProductCode{ get; set; }
    public DateTime ScheduledDate{ get; set; }
}