using System;
using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class AttendancePage : Page
    {
        private DiemDanhService service = new DiemDanhService();
        private int _userId;
        private string _vaiTro;

        public AttendancePage(int userId, string vaiTro)
        {
            InitializeComponent();

            _userId = userId;
            _vaiTro = vaiTro;

            LoadData();

            if (_vaiTro == "hs")
            {
                SetStudentMode();
            }
        }

        private void LoadData()
        {
            if (_vaiTro == "hs")
            {
                dgAttendance.ItemsSource = service.GetAttendanceByStudent(_userId);
            }
            else
            {
                dgAttendance.ItemsSource = service.GetAll();
            }
        }

        private void BtnCreateCode_Click(object sender, RoutedEventArgs e)
        {
            txtMaDiemDanh.Text = service.TaoMaDiemDanh();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            int lopId, hocSinhId;

            if (!int.TryParse(txtLopId.Text.Trim(), out lopId) ||
                !int.TryParse(txtHocSinhId.Text.Trim(), out hocSinhId))
            {
                MessageBox.Show("ID lớp và ID học sinh phải là số.");
                return;
            }

            if (cbBuoiHoc.SelectedItem == null || cbTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn buổi học và trạng thái.");
                return;
            }

            string buoiHoc = ((ComboBoxItem)cbBuoiHoc.SelectedItem).Content.ToString();
            string trangThai = ((ComboBoxItem)cbTrangThai.SelectedItem).Content.ToString();

            DiemDanh dd = new DiemDanh
            {
                LopId = lopId,
                HocSinhId = hocSinhId,
                GiaoVienId = null,
                NgayDiemDanh = DateTime.Now.Date,
                BuoiHoc = buoiHoc,
                TrangThai = trangThai,
                GhiChu = txtGhiChu.Text.Trim()
            };

            if (service.DiemDanhThuCong(dd))
            {
                MessageBox.Show("Điểm danh thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Điểm danh thất bại.");
            }
        }
        private void SetStudentMode()
        {
            InputPanel.Visibility = Visibility.Collapsed;
        }
    }
}
