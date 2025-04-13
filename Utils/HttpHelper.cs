using System.Text;

namespace MesApiServer.Utils;
public class HttpHelper {
    // 测试 Demo
    public static async Task<string> PostAsync(string url, string jsonData) {
        using HttpClient httpClient = new();

        StringContent content = new(jsonData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await httpClient.PostAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }
}