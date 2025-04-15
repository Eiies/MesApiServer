using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;
using static MesApiServer.Models.TrackInRequest;

namespace MesApiServer.Models;

public class TrackInRequest {

    public string From { get; set; } = "MES";
    public string Message { get; set; } = "上机请求";
    public string DateTime { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    public TrackInContent Content { get; set; } = new TrackInContent();

    public class TrackInContent {
        public string CarrierID { get; set; } = null!;
        public string EmployeeID { get; set; } = null!;
        public string EQPID { get; set; } = null!;
        public string LotID { get; set; } = null!;
    }
}