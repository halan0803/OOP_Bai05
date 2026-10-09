/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
namespace DeviceManagement
{
    public class Projector : Device
    {
        private int _brightnessLumens;
        public int BrightnessLumens
        {
            get => _brightnessLumens;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Độ sáng phải lớn hơn 0.");

                _brightnessLumens = value;
            }
        } 
        private int _lampHoursUsed;
        public int LampHoursUsed
        {
            get => _lampHoursUsed;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Số giờ sử dụng bóng đèn không được âm.");

                _lampHoursUsed = value;
            }
        }

        public Projector(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice, int brightnessLumens, int lampHoursUsed, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, deviceName, yearOfUse, purchasePrice, status)
        {
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