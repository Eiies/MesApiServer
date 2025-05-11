using ApiServer.Models;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiServer.Services;

public interface IRcsService {
    Task<BaseRcsResponse> GenerateAgvSchedulingTaskAsync(GenAgvSchedulingTaskRequest req);
}

public interface IAgvCallbackService {
    Task<AgvCallbackResponse> ProcessAgvCallbackAsync(AgvCallbackRequest callbackRequest);
}

public class RcsService(HttpClient httpClient, ILogger<RcsService> logger) :IRcsService {
    private const string RcsLiteBaseUrl = "http://10.126.56.30:8182/rcms/services/rest/hikRpcService";
    private const string GenAgvSchedulingTaskEndpoint = "/genAgvSchedulingTask";

    private static readonly JsonSerializerOptions JsonOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // 忽略 null 值
    };

    // curl -X POST http://10.126.56.30:8182/rcms/services/rest/hikRpcService/genAgvSchedulingTask

    public async Task<BaseRcsResponse> GenerateAgvSchedulingTaskAsync(GenAgvSchedulingTaskRequest req) {

        var jsonContent = JsonSerializer.Serialize(req, JsonOptions);
        using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var requestUrl = $"{RcsLiteBaseUrl}{GenAgvSchedulingTaskEndpoint}";

        try {
            logger.LogInformation("正在向……发送RCS请求 {Url} 带有有效载荷；带负载 {@Payload}", requestUrl, jsonContent);

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl) {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            var res = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if(res.IsSuccessStatusCode) {
                try {
                    var responseContent = await res.Content.ReadAsStringAsync();
                    logger.LogInformation("源内容: {Content}", responseContent);
                    var rcsResponse = JsonSerializer.Deserialize<BaseRcsResponse>(responseContent);
                    if(rcsResponse != null) {
                        return rcsResponse;
                    }
                    return new BaseRcsResponse { Code = "CLIENT_ERROR", Message = "反序列化RCS响应失败", ReqCode = string.Empty };
                } catch(Exception) {
                    logger.LogError("反序列化RCS响应失败");
                    return new BaseRcsResponse { Code = "CLIENT_ERROR", Message = "反序列化RCS响应失败", ReqCode = string.Empty };
                }
            } else {
                var errorContent = await res.Content.ReadAsStringAsync();
                logger.LogError($"RCS请求失败: {res.StatusCode}, 内容: {errorContent}");
                return new BaseRcsResponse { Code = res.StatusCode.ToString(), Message = $"无法连接到 RCS {errorContent}", ReqCode = string.Empty };
            }
        } catch(HttpRequestException httpEx) {
            var errorType = httpEx.InnerException switch {
                HttpIOException => "NETWORK_INTERRUPTION",
                SocketException => "SOCKET_ERROR",
                _ => "HTTP_ERROR"
            };
            logger.LogError(httpEx, $"HTTP请求错误{errorType}");
            return new BaseRcsResponse { Code = "HTTP_ERROR", Message = $"HTTP request error: {httpEx.Message}", ReqCode = string.Empty };
        } catch(JsonException jsonEx) {
            logger.LogError(jsonEx, "JSON处理错误");
            return new BaseRcsResponse { Code = "JSON_ERROR", Message = $"JSON processing error: {jsonEx.Message}", ReqCode = string.Empty };
        } catch(Exception ex) {
            logger.LogError(ex, "未知错误");
            return new BaseRcsResponse { Code = "UNKNOWN_ERROR", Message = $"An unexpected error occurred: {ex.Message}", ReqCode = string.Empty };
        }
    }
}

public class AgvCallbackService(ILogger<AgvCallbackService> logger) :IAgvCallbackService {
    public Task<AgvCallbackResponse> ProcessAgvCallbackAsync(AgvCallbackRequest callbackRequest) {
        logger.LogInformation("Received AGV callback. Method: {Method}, TaskCode: {TaskCode}, RobotCode: {RobotCode}, ReqCode: {ReqCode}",
            callbackRequest.Method, callbackRequest.TaskCode, callbackRequest.RobotCode, callbackRequest.ReqCode);

        switch(callbackRequest.Method?.ToLowerInvariant()) {
            case "start":
                logger.LogInformation("Task {TaskCode} started for robot {RobotCode} at {Position}",
                    callbackRequest.TaskCode, callbackRequest.RobotCode, callbackRequest.CurrentPositionCode);
                break;
            case "end":
                logger.LogInformation("Task {TaskCode} ended for robot {RobotCode} at {Position}",
                    callbackRequest.TaskCode, callbackRequest.RobotCode, callbackRequest.CurrentPositionCode);
                break;
            default:
                logger.LogWarning("Received unknown AGV callback method: {Method}", callbackRequest.Method);
                return Task.FromResult(new AgvCallbackResponse {
                    Code = "CALLBACK_ERROR",
                    Message = $"Unknown method: {callbackRequest.Method}",
                    ReqCode = callbackRequest.ReqCode
                });
        }

        var response = new AgvCallbackResponse {
            Code = "0",
            Message = "成功",
            ReqCode = callbackRequest.ReqCode
        };
        return Task.FromResult(response);
    }
}
