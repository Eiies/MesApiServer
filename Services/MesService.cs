namespace MesApiServer.Services;

public class MesService(HttpClient httpClient){
    public async Task<string> GetExternalDataAsync(){
        var response = await httpClient.GetAsync("https://api.example.com/data");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}