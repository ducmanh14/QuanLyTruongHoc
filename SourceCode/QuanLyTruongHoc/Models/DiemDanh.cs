using System;

namespace QuanLyTruongHoc.Models
{
    public class DiemDanh
    {
        public int Id { get; set; }

        public int LopId { get; set; }

        public int HocSinhId { get; set; }

        public int? GiaoVienId { get; set; }

        // Hiển thị
        public string TenLop { get; set; }

        public string MaHS { get; set; }

        public string HoTen { get; set; }

        // Thông tin điểm danh
        public DateTime NgayDiemDanh { get; set; }

        public string BuoiHoc { get; set; }

        public string TrangThai { get; set; }

        public string GhiChu { get; set; }

        public DateTime NgayTao { get; set; }

        // ===============================
        // THÊM MỚI CHO ĐIỂM DANH BẰNG MÃ
        // ===============================

        // Mã điểm danh giáo viên tạo
        public string MaDiemDanh { get; set; }

        // Hạn sử dụng mã
        public DateTime? HetHan { get; set; }

        // Đã sử dụng chưa
        public bool DaSuDung { get; set; }
    }
}