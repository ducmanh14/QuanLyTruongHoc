using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class TeacherManagementPage : Page
    {
        private GiaoVienService service = new GiaoVienService();
        private GiaoVien selectedTeacher = null;

        public TeacherManagementPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgTeachers.ItemsSource = service.GetAll();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            dgTeachers.ItemsSource = string.IsNullOrEmpty(keyword) ? service.GetAll() : service.Search(keyword);
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private GiaoVien GetFormData()
        {
            if (string.IsNullOrWhiteSpace(txtMaGV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập mã giáo viên, họ tên và email.");
                return null;
            }
            return new GiaoVien
            {
                MaGV = txtMaGV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                ChuyenMon = txtChuyenMon.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim()
            };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            GiaoVien gv = GetFormData();
            if (gv == null) return;

            if (service.Add(gv))
            {
                MessageBox.Show("Thêm giáo viên thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm giáo viên thất bại. Có thể mã giáo viên hoặc email đã tồn tại.");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTeacher == null)
            {
                MessageBox.Show("Vui lòng chọn giáo viên cần sửa.");
                return;
            }

            GiaoVien gv = GetFormData();
            if (gv == null) return;

            gv.Id = selectedTeacher.Id;
            gv.UserId = selectedTeacher.UserId;

            if (service.Update(gv))
            {
                MessageBox.Show("Cập nhật giáo viên thành công!");
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
            if (selectedTeacher == null)
            {
                MessageBox.Show("Vui lòng chọn giáo viên cần xóa.");
                return;
            }

            if (service.Delete(selectedTeacher.UserId))
            {
                MessageBox.Show("Xóa giáo viên thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Xóa thất bại.");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void DgTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTeacher = dgTeachers.SelectedItem as GiaoVien;
            if (selectedTeacher != null)
            {
                txtMaGV.Text = selectedTeacher.MaGV;
                txtHoTen.Text = selectedTeacher.HoTen;
                txtEmail.Text = selectedTeacher.Email;
                txtChuyenMon.Text = selectedTeacher.ChuyenMon;
                txtSoDienThoai.Text = selectedTeacher.SoDienThoai;
            }
        }

        private void ClearForm()
        {
            selectedTeacher = null;
            txtSearch.Clear();
            txtMaGV.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtChuyenMon.Clear();
            txtSoDienThoai.Clear();
            dgTeachers.SelectedItem = null;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}