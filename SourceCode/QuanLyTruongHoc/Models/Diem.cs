using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTruongHoc.Models
{
    public class Diem
    {
        public int Id { get; set; }
        public int HocSinhId { get; set; }
        public int LopId { get; set; }
        public int MonHocId { get; set; }
        public int? GiaoVienId { get; set; }

        public string MaHS { get; set; }
        public string HoTen { get; set; }
        public string TenLop { get; set; }
        public string TenMon { get; set; }

        public string LoaiDiem { get; set; }
        public decimal GiaTri { get; set; }
        public int HocKy { get; set; }
        public string NamHoc { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
