using System.Text;

namespace MesApiServer.Utils;

public class HttpHelper {
    // 测试 Demo
    public static async Task<string> PostAsync(string url, string jsonData) {
        using var httpClient = new HttpClient();

        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }
}