/***************/
// Mã sinh viên: 202418931
// Họ tên: Trần Thị Hà Lan
/***************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagement
{
    public class LabRoom
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public List<Device> Devices { get; set; }

        public LabRoom(string roomId, string roomName, int capacity)
        {
            if (string.IsNullOrWhiteSpace(roomId)) 
                throw new ArgumentException("Mã phòng không được rỗng.");

            if (string.IsNullOrWhiteSpace(roomName)) 
                throw new ArgumentException("Tên phòng không được rỗng.");

            if (capacity <= 0) 
                throw new ArgumentOutOfRangeException(nameof(capacity), "Sức chứa phải lớn hơn 0.");

            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
            Devices = new List<Device>();
        }

        // Thêm thiết bị vào phòng thực hành
        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Không thể thêm thiết bị null vào phòng.");

            if (FindDevice(device.DeviceId) != null)
                throw new InvalidOperationException($"Thiết bị có mã '{device.DeviceId}' đã tồn tại trong phòng {RoomId}.");

            Devices.Add(device);
        }

        // Xóa thiết bị khỏi phòng theo mã
        public bool RemoveDevice(string deviceId)
        {
            var device = FindDevice(deviceId);
            if (device == null) return false;
            return Devices.Remove(device);
        }

        // Tìm thiết bị trong phòng theo mã
        public Device? FindDevice(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) return null;
            return Devices.FirstOrDefault(d => d.DeviceId.Equals(deviceId.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        // Tính tổng chi phí bảo trì bằng tính đa hình
        public decimal CalculateAnnualMaintenanceCost()
        {
            decimal total = 0;
            foreach (var device in Devices)
            {
                total += device.CalculateMaintenanceCost(); // Đa hình động 
            }
            return total;
        }

        // Lấy danh sách thiết bị đang UnderMaintenance hoặc đã sử dụng trên 5 năm
        public List<Device> GetDevicesRequiringMaintenance()
        {
            return Devices
                .Where(d => d.Status == DeviceStatus.UnderMaintenance || DateTime.Now.Year - d.YearOfUse > 5)
                .ToList();
        }
    }
}