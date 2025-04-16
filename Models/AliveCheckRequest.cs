namespace MesApiServer.Models;

public class AliveCheckRequest {

    public string From { get; set; } = "MES";

    public string Message { get; set; } = "AliveCheckRequest";

    public AliveCheckContext Content { get; set; } = new AliveCheckContext();

    public string DateTime { get; set; } = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

    public class AliveCheckContext {
        public string EQPID { get; set; } = null!;
    }
}


