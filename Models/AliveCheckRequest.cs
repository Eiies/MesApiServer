namespace MesApiServer.Models;

public class AliveCheckRequest {

    public string Form { get; set; } = "AliveCheckRequest";

    public AliveCheckRContext Context { get; set; } = new AliveCheckRContext();

    public DateTime StartTime { get; set; }

    public class AliveCheckRContext {
        public string EQPID { get; set; } = null!;
    }
}


