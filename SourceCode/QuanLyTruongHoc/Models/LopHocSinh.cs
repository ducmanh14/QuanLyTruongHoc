using System;

namespace QuanLyTruongHoc.Models
{
    public class LopHocSinh
    {
        public int Id { get; set; }
        public int LopId { get; set; }
        public int HocSinhId { get; set; }
        public string TenLop { get; set; }
        public string MaHS { get; set; }
        public string HoTen { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayVaoLop { get; set; }
        public DateTime? NgayRoiLop { get; set; }
    }
}
