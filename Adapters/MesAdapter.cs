using ApiServer.Models;
using MQTTnet;
using System.Text;
using System.Text.Json;

namespace ApiServer.Adapters {
    public class MqttMesAdapter(ILogger<MqttMesAdapter> logger) :IMesAdapter {
        private readonly IMqttClient mqttClient = new MqttClientFactory().CreateMqttClient();

        private readonly MqttClientOptions options = new MqttClientOptionsBuilder()
            .WithClientId("MesApiServerClient")
            .WithTcpServer("localhost", 1883) // MQTT Broker 地址和端口
            .WithCleanSession()
            .Build();

        public void SendAliveNotification(AliveCheckRequest request) =>
            // 异步发布，可以不 await，但建议捕获异常
            _ = PublishAsync("mes/alive", request);

        public void SendMessage(string message) =>
            _ = PublishAsync("mes/message", new { Text = message, Time = DateTime.UtcNow });

        private async Task EnsureConnectedAsync() {
            if(!mqttClient.IsConnected) {
                MqttClientConnectResult? result = await mqttClient.ConnectAsync(options, CancellationToken.None);
                logger.LogInformation("已连接到 MQTT Broker，返回码：{ResultCode}", result.ResultCode);
            }
        }

        private async Task PublishAsync(string topic, object payload) {
            await EnsureConnectedAsync();
            string json = JsonSerializer.Serialize(payload);
            MqttApplicationMessage? message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(json))
                .WithRetainFlag(false)
                .Build();

            await mqttClient.PublishAsync(message, CancellationToken.None);
            logger.LogInformation("已发布消息到 MQTT 主题 {Topic}：{Payload}", topic, json);
        }
    }
}