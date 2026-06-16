using System;

namespace QuanLyTruongHoc.Models
{
    public class GiaoVien
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MaGV { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string ChuyenMon { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime NgayTao { get; set; }
    }
}