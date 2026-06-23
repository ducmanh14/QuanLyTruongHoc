using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class ClassManagementPage : Page
    {
        private LopHocService lopHocService = new LopHocService();
        private LopHoc selectedClass = null;

        public ClassManagementPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgClasses.ItemsSource = lopHocService.GetAll();
        }

        private LopHoc GetFormData()
        {
            if (string.IsNullOrWhiteSpace(txtTenLop.Text) ||
                string.IsNullOrWhiteSpace(txtKhoi.Text) ||
                string.IsNullOrWhiteSpace(txtNienKhoa.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên lớp, khối, niên khóa.");
                return null;
            }

            int siSo = 0;
            int.TryParse(txtSiSo.Text.Trim(), out siSo);

            return new LopHoc
            {
                TenLop = txtTenLop.Text.Trim(),
                Khoi = txtKhoi.Text.Trim(),
                NienKhoa = txtNienKhoa.Text.Trim(),
                SiSo = siSo
            };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LopHoc lop = GetFormData();
            if (lop == null) return;

            if (lopHocService.Add(lop))
            {
                MessageBox.Show("Thêm lớp thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm lớp thất bại.");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedClass == null)
            {
                MessageBox.Show("Vui lòng chọn lớp cần sửa.");
                return;
            }

            LopHoc lop = GetFormData();
            if (lop == null) return;

            lop.Id = selectedClass.Id;

            if (lopHocService.Update(lop))
            {
                MessageBox.Show("Cập nhật lớp thành công!");
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
            if (selectedClass == null)
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa.");
                return;
            }

            if (lopHocService.Delete(selectedClass.Id))
            {
                MessageBox.Show("Xóa lớp thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Xóa thất bại. Có thể lớp đang có học sinh.");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void DgClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedClass = dgClasses.SelectedItem as LopHoc;

            if (selectedClass != null)
            {
                txtTenLop.Text = selectedClass.TenLop;
                txtKhoi.Text = selectedClass.Khoi;
                txtNienKhoa.Text = selectedClass.NienKhoa;
                txtSiSo.Text = selectedClass.SiSo.ToString();
            }
        }

        private void ClearForm()
        {
            selectedClass = null;
            txtTenLop.Clear();
            txtKhoi.Clear();
            txtNienKhoa.Clear();
            txtSiSo.Clear();
            dgClasses.SelectedItem = null;
        }
    }
}
