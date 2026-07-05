using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class StudentManagementPage : Page
    {
        private HocSinhService hocSinhService = new HocSinhService();
        private HocSinh selectedHocSinh = null;

        private string _vaiTro;

        public StudentManagementPage(string vaiTro)
        {
            InitializeComponent();

            _vaiTro = vaiTro;

            LoadData();

            if (_vaiTro == "gv")
            {
                SetReadOnlyMode();
            }
        }

        private void SetReadOnlyMode()
        {
            // Ẩn các nút chỉnh sửa
            btnAdd.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            btnClear.Visibility = Visibility.Collapsed;

            // Khóa các ô nhập
            txtMaHS.IsReadOnly = true;
            txtHoTen.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtDiaChi.IsReadOnly = true;

            dpNgaySinh.IsEnabled = false;
            cbGioiTinh.IsEnabled = false;
        }

        private void LoadData()
        {
            dgStudents.ItemsSource = hocSinhService.GetAll();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            dgStudents.ItemsSource = string.IsNullOrEmpty(keyword)
                ? hocSinhService.GetAll()
                : hocSinhService.Search(keyword);
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            HocSinh hs = GetFormData();

            if (hs == null) return;

            if (hocSinhService.Add(hs))
            {
                MessageBox.Show("Thêm học sinh thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm học sinh thất bại. Có thể mã học sinh hoặc email đã tồn tại.");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedHocSinh == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh cần sửa.");
                return;
            }

            HocSinh hs = GetFormData();
            if (hs == null) return;

            hs.Id = selectedHocSinh.Id;
            hs.UserId = selectedHocSinh.UserId;

            if (hocSinhService.Update(hs))
            {
                MessageBox.Show("Cập nhật học sinh thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedHocSinh == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh cần xóa.");
                return;
            }

            MessageBoxResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa học sinh này?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                if (hocSinhService.Delete(selectedHocSinh.UserId))
                {
                    MessageBox.Show("Xóa học sinh thành công!");
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.");
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void DgStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedHocSinh = dgStudents.SelectedItem as HocSinh;

            if (selectedHocSinh != null)
            {
                txtMaHS.Text = selectedHocSinh.MaHS;
                txtHoTen.Text = selectedHocSinh.HoTen;
                txtEmail.Text = selectedHocSinh.Email;
                dpNgaySinh.SelectedDate = selectedHocSinh.NgaySinh;
                txtDiaChi.Text = selectedHocSinh.DiaChi;

                foreach (ComboBoxItem item in cbGioiTinh.Items)
                {
                    if (item.Content.ToString() == selectedHocSinh.GioiTinh)
                    {
                        cbGioiTinh.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private HocSinh GetFormData()
        {
            if (string.IsNullOrWhiteSpace(txtMaHS.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập mã học sinh, họ tên và email.");
                return null;
            }

            string gioiTinh = "";

            if (cbGioiTinh.SelectedItem != null)
            {
                gioiTinh = ((ComboBoxItem)cbGioiTinh.SelectedItem).Content.ToString();
            }

            return new HocSinh
            {
                MaHS = txtMaHS.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                NgaySinh = dpNgaySinh.SelectedDate,
                GioiTinh = gioiTinh,
                DiaChi = txtDiaChi.Text.Trim()
            };
        }

        private void ClearForm()
        {
            selectedHocSinh = null;

            txtSearch.Clear();
            txtMaHS.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();

            dpNgaySinh.SelectedDate = null;
            cbGioiTinh.SelectedIndex = -1;

            dgStudents.SelectedItem = null;
        }
    }
}