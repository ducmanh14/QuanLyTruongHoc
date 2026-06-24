using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTruongHoc.Models
{
    public class DiemDanh
    {
        public int Id { get; set; }
        public int LopId { get; set; }
        public int HocSinhId { get; set; }
        public int? GiaoVienId { get; set; }

        public string TenLop { get; set; }
        public string MaHS { get; set; }
        public string HoTen { get; set; }

        public DateTime NgayDiemDanh { get; set; }
        public string BuoiHoc { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
    }
}

