using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class TeachingAssignmentPage : Page
    {
        private PhanCongGiangDayService service = new PhanCongGiangDayService();
        private PhanCongGiangDay selectedAssignment = null;

        public TeachingAssignmentPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgAssignments.ItemsSource = service.GetAll();
        }

        private PhanCongGiangDay GetFormData()
        {
            int lopId, giaoVienId, monHocId, hocKy;
            if (!int.TryParse(txtLopId.Text.Trim(), out lopId) ||
                !int.TryParse(txtGiaoVienId.Text.Trim(), out giaoVienId) ||
                !int.TryParse(txtMonHocId.Text.Trim(), out monHocId) ||
                !int.TryParse(txtHocKy.Text.Trim(), out hocKy))
            {
                MessageBox.Show("ID lớp, ID giáo viên, ID môn học và học kỳ phải là số.");
                return null;
            }
            if (string.IsNullOrWhiteSpace(txtNamHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập năm học.");
                return null;
            }
            return new PhanCongGiangDay
            {
                LopId = lopId,
                GiaoVienId = giaoVienId,
                MonHocId = monHocId,
                HocKy = hocKy,
                NamHoc = txtNamHoc.Text.Trim()
            };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            PhanCongGiangDay pc = GetFormData();
            if (pc == null) return;

            if (service.Add(pc))
            {
                MessageBox.Show("Thêm phân công thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm phân công thất bại.");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAssignment == null)
            {
                MessageBox.Show("Vui lòng chọn phân công cần sửa.");
                return;
            }

            PhanCongGiangDay pc = GetFormData();
            if (pc == null) return;

            pc.Id = selectedAssignment.Id;
            if (service.Update(pc))
            {
                MessageBox.Show("Cập nhật phân công thành công!");
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
            if (selectedAssignment == null)
            {
                MessageBox.Show("Vui lòng chọn phân công cần xóa.");
                return;
            }

            if (service.Delete(selectedAssignment.Id))
            {
                MessageBox.Show("Xóa phân công thành công!");
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

        private void DgAssignments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAssignment = dgAssignments.SelectedItem as PhanCongGiangDay;
            if (selectedAssignment != null)
            {
                txtLopId.Text = selectedAssignment.LopId.ToString();
                txtGiaoVienId.Text = selectedAssignment.GiaoVienId.ToString();
                txtMonHocId.Text = selectedAssignment.MonHocId.ToString();
                txtHocKy.Text = selectedAssignment.HocKy.ToString();
                txtNamHoc.Text = selectedAssignment.NamHoc;
            }
        }

        private void ClearForm()
        {
            selectedAssignment = null;
            txtLopId.Clear();
            txtGiaoVienId.Clear();
            txtMonHocId.Clear();
            txtHocKy.Clear();
            txtNamHoc.Clear();
            dgAssignments.SelectedItem = null;
        }
    }
}