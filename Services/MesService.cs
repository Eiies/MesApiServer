using System.Text;
using System.Text.Json;
using MesApiServer.Models;

namespace MesApiServer.Services;

public class MesService(HttpClient httpClient){
    public async Task<bool> SendToMesAsync(WorkOrderDto dto){
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("http://mes-server/api/receive", content);
        return response.IsSuccessStatusCode;
    }
}