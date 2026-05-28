using System.Windows;

namespace QuanLyTruongHoc
{
    public partial class MainWindow : Window
    {
        private string _hoTen;
        private string _vaiTro;

        public MainWindow(string hoTen, string vaiTro)
        {
            InitializeComponent();

            _hoTen = hoTen;
            _vaiTro = vaiTro;

            txtWelcome.Text = "Xin chào, " + _hoTen;
            txtRole.Text = "Vai trò: " + _vaiTro;

            ApplyPermission();
        }

        private void ApplyPermission()
        {
            if (_vaiTro == "admin")
            {
                return;
            }

            if (_vaiTro == "gv")
            {
                btnClass.Visibility = Visibility.Collapsed;
                btnTeacher.Visibility = Visibility.Collapsed;
                btnSubject.Visibility = Visibility.Collapsed;
                btnAssignment.Visibility = Visibility.Collapsed;
            }

            if (_vaiTro == "hs")
            {
                btnStudent.Visibility = Visibility.Collapsed;
                btnClass.Visibility = Visibility.Collapsed;
                btnTeacher.Visibility = Visibility.Collapsed;
                btnSubject.Visibility = Visibility.Collapsed;
                btnAssignment.Visibility = Visibility.Collapsed;
                btnGrade.Content = "Xem điểm";
                btnAttendance.Content = "Nhập mã điểm danh";
            }
        }
    }
}