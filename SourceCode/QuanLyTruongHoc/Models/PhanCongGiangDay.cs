namespace QuanLyTruongHoc.Models
{
    public class PhanCongGiangDay
    {
        public int Id { get; set; }
        public int LopId { get; set; }
        public int GiaoVienId { get; set; }
        public int MonHocId { get; set; }
        public int HocKy { get; set; }
        public string NamHoc { get; set; }
        public string TenLop { get; set; }
        public string TenGiaoVien { get; set; }
        public string TenMon { get; set; }
    }
}