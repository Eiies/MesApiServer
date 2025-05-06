namespace ApiServer.Models {
    public class TrackInRequest {
        public string From { get; set; } = "MES";
        public string Message { get; set; } = "TrackInRequest";
        public string DateTime { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public TrackInContent Content { get; set; } = new();

        public class TrackInContent {
            public string CarrierID { get; set; } = null!;
            public string EmployeeID { get; set; } = null!;
            public string EQPID { get; set; } = null!;
            public string LotID { get; set; } = null!;
        }
    }
}