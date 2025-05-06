namespace ApiServer.Models {
    public class ProcessEndRequest {
        public string From { get; set; } = "MES";
        public string Message { get; set; } = "ProcessEnd";
        public string DateTime { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public EndContent Content { get; set; } = new();

        public class EndContent {
            public string CarrierID { get; set; } = null!;
            public string EQPID { get; set; } = null!;
            public string LotID { get; set; } = null!;
        }
    }
}