using System;

namespace QuanLyTruongHoc.Models
{
    public class HocSinh
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MaHS { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string TenLop { get; set; }
        public string TrangThai { get; set; }
    }
}
