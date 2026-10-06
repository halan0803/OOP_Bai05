/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
namespace DeviceManagement
{
    public abstract class Device
    {
        public string DeviceID { get; init; }
        public string DeviceName { get; set; }
        public int YearOfUse { get; set; }
        public decimal PurchasePrice { get; set; }
        public DeviceStatus Status { get; set; }

        public Device(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice, DeviceStatus status = DeviceStatus.Active)
        {
            // Mã thiết bị không được rỗng
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("Mã thiết bị không được rỗng.");

            // Giá mua phải lớn hơn 0
            if (purchasePrice <= 0)
                throw new ArgumentException("Giá mua phải lớn hơn 0.");

            // Năm đưa vào sử dụng không được lớn hơn năm hiện tại
            if (yearOfUse > DateTime.Now.Year)
                throw new ArgumentException("Năm đưa vào sử dụng không được lớn hơn năm hiện tại.");
            
            DeviceID = deviceId;
            DeviceName = deviceName;
            YearOfUse = yearOfUse;
            PurchasePrice = purchasePrice;
            Status = status;
        }
    
        // Phương thức trừu tượng tính chi phí bảo trì dư kiến trong 1 năm
        public abstract decimal CalculateMaintenanceCost();

        // Ghi đè phương thức biểu diễn thông tin của thiết bị dưới dạng chuỗi
        public override string ToString()
            {
                return $"[{DeviceID}] {DeviceName} | Năm SD: {YearOfUse} | Giá mua: {PurchasePrice:N0} đ | Trạng thái: {Status} | Phí bảo trì: {CalculateMaintenanceCost():N0} đ";
            }
    }
}