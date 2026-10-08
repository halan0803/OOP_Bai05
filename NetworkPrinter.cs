/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
namespace DeviceManagement
{
    // NetworkPrinter kế thừa từ Printer và thực thi INetworkable
    public class NetworkPrinter : Printer, INetworkable
    {
        public bool SupportNetwork => true;
        public string IpAddress { get; private set; } = string.Empty;
        public bool IsConnected { get; private set; } = false;

        public NetworkPrinter(string deviceId, string name, int yearOfUse, decimal purchasePrice,
                              PrinterType type, int printedPages, bool isColorPrinter, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, name, yearOfUse, purchasePrice, type, printedPages, true, isColorPrinter, status)
        {
        }

        public void Connect(string ipAddress)
        {
            // Địa chỉ IP không được rỗng
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("Địa chỉ IP không được rỗng.");
            // Không cho phép kết nối lại khi thiết bị đang kết nối
            if (IsConnected)
                throw new InvalidOperationException($"Máy in mạng [{DeviceID}] đang kết nối, không thể kết nối lại.");

            IpAddress = ipAddress.Trim();
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
            IpAddress = string.Empty; // Xóa địa chỉ IP đang hoạt động sau khi ngắt kết nối
        }

        public override string ToString()
        {
            string netInfo = IsConnected ? $"Đã kết nối ({IpAddress})" : "Chưa kết nối";
            return $"[{DeviceID}] {DeviceName} | Năm SD: {YearOfUse} | Giá mua: {PurchasePrice:N0} đ | Loại máy in: {Type} | Số trang đã in: {PrintedPages} trang | Máy in màu: {(IsColorPrinter ? "Có" : "Không")} | Trạng thái: {Status} | Phí bảo trì: {CalculateMaintenanceCost():N0} đ | [Trạng thái mạng: {netInfo}]";
        }
    }
}