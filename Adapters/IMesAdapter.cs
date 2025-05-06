using ApiServer.Models;

namespace ApiServer.Adapters {
    public interface IMesAdapter {
        // 发送消息到MES
        void SendMessage(string message);

        void SendAliveNotification(AliveCheckRequest request);
    }
}