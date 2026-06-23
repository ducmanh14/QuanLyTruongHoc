using System;

namespace QuanLyTruongHoc.Models
{
    public class LopHoc
    {
        public int Id { get; set; }
        public int? GvChuNhiemId { get; set; }
        public string TenLop { get; set; }
        public string Khoi { get; set; }
        public string NienKhoa { get; set; }
        public int SiSo { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
