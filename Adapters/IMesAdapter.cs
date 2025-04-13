namespace MesApiServer.Adapters;
public interface IMesAdapter {
    // 发送消息到MES
    void SendMessage(string message);
}
