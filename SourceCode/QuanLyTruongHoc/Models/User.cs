namespace QuanLyTruongHoc.Models
{
    public class User
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }
        public bool TrangThai { get; set; }
    }
}
