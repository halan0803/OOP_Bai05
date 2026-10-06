/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
namespace DeviceManagement
{
    public class Computer : Device
    {
        public int Ram { get; set; } 
        public string CpuType { get; set; } 
        public bool HasDedicatedGpu { get; set; }

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
    }
}