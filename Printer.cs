/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
namespace DeviceManagement
{
    public enum PrinterType
    {
        Laser,
        Inkjet // Máy in phun
    }

    public class Printer : Device
    {
        public PrinterType Type { get; set; } 
        private int _printedPages;
        public int PrintedPages
        {
            get => _printedPages;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Số trang đã in không được âm.");

                _printedPages = value;
            }
        } 
        public virtual bool SupportNetwork => false; 
        public bool IsColorPrinter {get; set; }

        public Printer(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice, PrinterType type, int printedPages, bool supportNetwork, bool isColorPrinter, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, deviceName, yearOfUse, purchasePrice, status)
        {
            Type = type;
            PrintedPages = printedPages;
            IsColorPrinter = isColorPrinter;
        }

        // Ghi đè phương thức tính chi phí bảo trì 
        public override decimal CalculateMaintenanceCost()
        {
            // 4% giá mua
            decimal cost = PurchasePrice * 0.04m;
            // Cộng thêm 500.000 đồng nếu số trang đã in lớn hơn 100.000
            if (PrintedPages > 100000)
                cost += 500000m;
            // Cộng thêm 300.000 đồng nếu là máy in màu
            if (IsColorPrinter)
                cost += 300000m;
            return cost; 
        }
        public override string ToString()
        {
            return $"[{DeviceID}] {DeviceName} | Năm SD: {YearOfUse} | Giá mua: {PurchasePrice:N0} đ | Loại máy in: {Type} | Số trang đã in: {PrintedPages} trang | Hỗ trợ mạng: {(SupportNetwork ? "Có" : "Không")} | Máy in màu: {(IsColorPrinter ? "Có" : "Không")} | Trạng thái: {Status} | Phí bảo trì: {CalculateMaintenanceCost():N0} đ";
        }
    }
}