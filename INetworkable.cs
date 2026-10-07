/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
namespace DeviceManagement
{
    // interface INetworkable biểu diễn khả năng kết nối mạng
    public interface INetworkable 
    { 
        string IpAddress { get; } 
        void Connect(string ipAddress); 
        void Disconnect(); 
        bool IsConnected { get; } 
    }
}