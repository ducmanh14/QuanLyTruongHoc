using System.Windows.Controls;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class ProfilePage : Page
    {
        private ProfileService service = new ProfileService();

        public ProfilePage(int userId, string vaiTro)
        {
            InitializeComponent();
            LoadProfile(userId, vaiTro);
        }

        private void LoadProfile(int userId, string vaiTro)
        {
            var profile = service.GetProfile(userId, vaiTro);

            if (profile == null) return;

            txtHoTen.Text = "Họ tên: " + profile.HoTen;
            txtEmail.Text = "Email: " + profile.Email;
            txtVaiTro.Text = "Vai trò: " + profile.VaiTro;

            if (vaiTro == "hs")
            {
                txtMa.Text = "Mã học sinh: " + profile.MaHS;
                txtThongTinThem.Text = "Lớp: " + profile.TenLop;
            }
            else if (vaiTro == "gv")
            {
                txtMa.Text = "Mã giáo viên: " + profile.MaGV;
                txtThongTinThem.Text = "Chuyên môn: " + profile.ChuyenMon + " - SĐT: " + profile.SoDienThoai;
            }
            else
            {
                txtMa.Text = "Tài khoản quản trị";
                txtThongTinThem.Text = "";
            }
        }
    }
}
