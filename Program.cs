/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
using System.Collections.Generic;
using System.Text;
namespace DeviceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== HỆ THỐNG QUẢN LÝ THIẾT BỊ PHÒNG THỰC HÀNH ===\n");

            // Khởi tạo 2 máy tính (1 máy có GPU rời, 1 máy dùng > 5 năm)
            var pc1 = new Computer("PC01", "Dell Precision 3660", 2024, 30_000_000m, 32, "Core i7-13700", hasDedicatedGpu: true);
            var pc2 = new Computer("PC02", "HP ProDesk 400 G6", 2019, 15_000_000m, 16, "Core i5-9500", hasDedicatedGpu: false);

            // Khởi tạo 2 máy in (1 máy in mạng đã in > 100.000 trang, 1 máy in thường)
            var pr1 = new NetworkPrinter("PR01", "HP LaserJet Enterprise M611", 2022, 20_000_000m, PrinterType.Laser, printedPages: 125_000, isColorPrinter: false);
            var pr2 = new Printer("PR02", "Epson EcoTank L3210", 2023, 5_000_000m, PrinterType.Inkjet, printedPages: 15_000, supportNetwork: false, isColorPrinter: true, status: DeviceStatus.UnderMaintenance);

            // Khởi tạo 1 máy chiếu có bóng đèn đã sử dụng > 3.000 giờ
            var pj1 = new Projector("PJ01", "Sony VPL-EX455", 2020, 18_000_000m, brightnessLumens: 3600, lampHoursUsed: 3_500);

            // Khởi tạo 2 phòng thực hành
            var labA = new LabRoom("LAB-101", "Phòng Thực hành Lập trình", 40);
            var labB = new LabRoom("LAB-102", "Phòng Thực hành Đồ họa & Mạng", 30);

            // 1. Thêm thiết bị vào phòng
            Console.WriteLine("---> [Kiểm thử 1] Thêm thiết bị vào phòng:");
            labA.AddDevice(pc1);
            labA.AddDevice(pr1);
            labA.AddDevice(pj1);
            labB.AddDevice(pc2);
            labB.AddDevice(pr2);
            Console.WriteLine("    Đã thêm thành công các thiết bị vào 2 phòng Lab.\n");

            // 2. Thử thêm một thiết bị bị trùng mã
            Console.WriteLine("--> [Kiểm thử 2] Thử thêm thiết bị trùng mã 'PC01' vào phòng LAB-101:");
            try
            {
                var duplicatePc = new Computer("PC01", "Máy tính trùng mã", 2025, 12_000_000m, 8, "Core i3", false);
                labA.AddDevice(duplicatePc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    [Bắt lỗi thành công]: {ex.Message}\n");
            }

            // 3 & 4. In danh sách thiết bị và tính tổng chi phí bảo trì dự kiến của mỗi phòng
            Console.WriteLine("---> [Kiểm thử 3 & 4] In danh sách thiết bị & Tính tổng chi phí bảo trì:");
            var rooms = new List<LabRoom> { labA, labB };
            foreach (var room in rooms)
            {
                Console.WriteLine($"==================================================");
                Console.WriteLine($"PHÒNG: {room.RoomName} ({room.RoomId}) - Sức chứa: {room.Capacity}");
                Console.WriteLine("Danh sách thiết bị:");
                foreach (var dev in room.Devices)
                {
                    Console.WriteLine("  - " + dev);
                }
                Console.WriteLine($"=> Tổng chi phí bảo trì dự kiến/năm: {room.CalculateAnnualMaintenanceCost():N0} VNĐ\n");
            }

            // 5. Liệt kê thiết bị cần bảo trì trong từng phòng
            Console.WriteLine("--> [Kiểm thử 5] Danh sách thiết bị cần bảo trì (UnderMaintenance hoặc > 5 năm):");
            foreach (var room in rooms)
            {
                Console.WriteLine($"  * Phòng {room.RoomId}:");
                var maintDevices = room.GetDevicesRequiringMaintenance();
                foreach (var d in maintDevices)
                {
                    Console.WriteLine($"    + [{d.DeviceID}] {d.DeviceName} (Trạng thái: {d.Status}, Số năm SD: {DateTime.Now.Year - d.YearOfUse} năm)");
                }
            }
            Console.WriteLine();

            // 6 & 7. Kết nối mạng và duyệt các thiết bị thông qua kiểu INetworkable
            Console.WriteLine("--> [Kiểm thử 6 & 7] Quản lý kết nối mạng đa hình qua INetworkable:");
            List<INetworkable> networkDevices = new List<INetworkable> { pc1, pc2, pr1 };

            string[] sampleIps = { "192.168.1.10", "192.168.1.11", "192.168.1.200" };
            for (int i = 0; i < networkDevices.Count; i++)
            {
                networkDevices[i].Connect(sampleIps[i]);
            }

            // Duyệt qua kiểu INetworkable mà không phụ thuộc lớp cụ thể
            foreach (INetworkable netDev in networkDevices)
            {
                Console.WriteLine($"  - Đối tượng ({netDev.GetType().Name}) | Đang kết nối: {netDev.IsConnected} | IP: {netDev.IpAddress}");
            }

            // Kiểm thử ngắt kết nối
            Console.WriteLine("--> [Kiểm thử 8] Thử ngắt kết nối thiết bị đầu tiên");
            networkDevices[0].Disconnect();
            Console.WriteLine($"  - Sau khi Disconnect(): IsConnected = {networkDevices[0].IsConnected}, IpAddress = '{networkDevices[0].IpAddress}'");

            Console.WriteLine("\n============== HOÀN THÀNH KIỂM THỬ ==============");
            Console.ReadLine();
        }
    }
}