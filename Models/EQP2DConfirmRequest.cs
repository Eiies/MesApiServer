namespace ApiServer.Models {
    public class EQP2DConfirmRequest {
        public string From { get; set; } = "MES";
        public string Message { get; set; } = "EQP2DConfirmRequest";

        public EQP2DContext Content { get; set; } = new();

        public string DateTime { get; set; } = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        public class EQP2DContext {
            public string LotID { get; set; } = null!;
            public string EQPID { get; set; } = null!;
            public string EQP2DID { get; set; } = null!;
            public string Orientation { get; set; } = null!;
            public string RotationAngle { get; set; } = null!;
            public string ConnectMode { get; set; } = null!;
        }
    }
}