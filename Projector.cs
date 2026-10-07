/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
namespace DeviceManagement
{
    public class Projector : Device
    {
        public int BrightnessLumens { get; set; } 
        public int LampHoursUsed { get; set; } 

        public Projector(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice, int brightnessLumens, int lampHoursUsed, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, deviceName, yearOfUse, purchasePrice, status)
        {
            if (brightnessLumens <= 0)
                throw new ArgumentException("Độ sáng (Lumen) phải lớn hơn 0.");
            
            if (lampHoursUsed < 0)
                throw new ArgumentException("Số giờ đã sử dụng bóng đèn không được nhỏ hơn 0.");

            BrightnessLumens = brightnessLumens;
            LampHoursUsed = lampHoursUsed;
        }

        // Ghi đè phương thức tính chi phí bảo trì
        public override decimal CalculateMaintenanceCost()
        {
            // 3% giá mua
            decimal cost = PurchasePrice * 0.03m;
            // Cộng thêm 1.500.000 đồng nếu bóng đèn đã sử dụng trên 3.000 giờ
            if (LampHoursUsed > 3000)
                cost += 1500000m;
            return cost;
        }
        public override string ToString()
        {
            return $"[{DeviceID}] {DeviceName} | Năm SD: {YearOfUse} | Giá mua: {PurchasePrice:N0} đ | Độ sáng: {BrightnessLumens} Lumen | Số giờ đã sử dụng bóng đèn: {LampHoursUsed} giờ | Trạng thái: {Status} | Phí bảo trì: {CalculateMaintenanceCost():N0} đ";
        }
    }
}