/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
namespace DeviceManagement
{
    // Computer kế thừa từ Device và thực thi INetworkable
    public class Computer : Device, INetworkable
    {
        public int Ram { get; set; } 
        public string CpuType { get; set; } 
        public bool HasDedicatedGpu { get; set; }

        public string IpAddress { get; private set; } = string.Empty;
        public bool IsConnected { get; private set; } = false;

        public Computer(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice, int ram, string cpuType, bool hasDedicatedGpu, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, deviceName, yearOfUse, purchasePrice, status)
        {
            if(ram <= 0)
                throw new ArgumentException("Dung lượng RAM phải lớn hơn 0.");
            
            if(string.IsNullOrWhiteSpace(cpuType))
                throw new ArgumentException("Loại CPU không được rỗng.");

            Ram = ram;
            CpuType = cpuType;
            HasDedicatedGpu = hasDedicatedGpu;
        }

        // Ghi đè phương thức tính chi phí bảo trì
        public override decimal CalculateMaintenanceCost()
        {
            // 5% giá mua
            decimal rate = 0.05m;
            // Cộng thêm 2% giá mua nếu có GPU rời
            if (HasDedicatedGpu)
                rate += 0.02m;
            // Cộng thêm 1% giá mua nếu thiết bị đã sử dụng trên 5 năm
            if(DateTime.Now.Year - YearOfUse > 5)
                rate += 0.01m; 
            return PurchasePrice * rate;
        }

        public void Connect(string ipAddress)
        {
            // Địa chỉ IP không được rỗng
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("Địa chỉ IP không được rỗng.");
            // Không cho phép kết nối lại khi thiết bị đang kết nối
            if (IsConnected)
                throw new InvalidOperationException($"Máy tính [{DeviceID}] đang kết nối mạng rồi, không thể kết nối lại.");

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
            return $"[{DeviceID}] {DeviceName} | Năm SD: {YearOfUse} | Giá mua: {PurchasePrice:N0} đ | Dung lượng RAM: {Ram} GB | Loại CPU: {CpuType} | Có GPU rời: {(HasDedicatedGpu ? "Có" : "Không")} | Trạng thái: {Status} | Phí bảo trì: {CalculateMaintenanceCost():N0} đ | [Trạng thái mạng: {netInfo}]";
        }
    }
}