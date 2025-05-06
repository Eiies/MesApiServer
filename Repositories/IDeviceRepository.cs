using ApiServer.Data.Entities;
using ApiServer.Models;

namespace ApiServer.Repositories {
    public interface IDeviceRepository {
        /// <summary>
        ///     保存或更新设备心跳信息
        /// </summary>
        /// <param name="device">设备实体</param>
        void SaveHeartbeat(Device device);

        /// <summary>
        ///     保存上机请求记录（示例实现：当前仅记录日志）
        /// </summary>
        /// <param name="request">TrackIn 请求数据</param>
        void SaveTrackIn(TrackInRequest request);

        /// <summary>
        ///     保存设备确认（2D）请求记录
        /// </summary>
        /// <param name="request">确认请求数据</param>
        void SaveEQPConfirm(EQP2DConfirmRequest request);

        /// <summary>
        ///     保存工序结束请求记录
        /// </summary>
        /// <param name="request">结束请求数据</param>
        void SaveProcessEnd(ProcessEndRequest request);
    }
}