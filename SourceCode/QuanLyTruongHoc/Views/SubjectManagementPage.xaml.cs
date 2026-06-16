using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class SubjectManagementPage : Page
    {
        private MonHocService service = new MonHocService();
        private MonHoc selectedSubject = null;

        public SubjectManagementPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgSubjects.ItemsSource = service.GetAll();
        }

        private MonHoc GetFormData()
        {
            if (string.IsNullOrWhiteSpace(txtMaMon.Text) || string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập mã môn và tên môn.");
                return null;
            }
            int soTiet = 0;
            int.TryParse(txtSoTiet.Text.Trim(), out soTiet);
            return new MonHoc
            {
                MaMon = txtMaMon.Text.Trim(),
                TenMon = txtTenMon.Text.Trim(),
                SoTiet = soTiet
            };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MonHoc mh = GetFormData();
            if (mh == null) return;

            if (service.Add(mh))
            {
                MessageBox.Show("Thêm môn học thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm môn học thất bại.");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSubject == null)
            {
                MessageBox.Show("Vui lòng chọn môn học cần sửa.");
                return;
            }

            MonHoc mh = GetFormData();
            if (mh == null) return;

            mh.Id = selectedSubject.Id;
            if (service.Update(mh))
            {
                MessageBox.Show("Cập nhật môn học thành công!");
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
            if (selectedSubject == null)
            {
                MessageBox.Show("Vui lòng chọn môn học cần xóa.");
                return;
            }

            if (service.Delete(selectedSubject.Id))
            {
                MessageBox.Show("Xóa môn học thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Xóa thất bại. Có thể môn học đang được phân công.");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void DgSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSubject = dgSubjects.SelectedItem as MonHoc;
            if (selectedSubject != null)
            {
                txtMaMon.Text = selectedSubject.MaMon;
                txtTenMon.Text = selectedSubject.TenMon;
                txtSoTiet.Text = selectedSubject.SoTiet.ToString();
            }
        }

        private void ClearForm()
        {
            selectedSubject = null;
            txtMaMon.Clear();
            txtTenMon.Clear();
            txtSoTiet.Clear();
            dgSubjects.SelectedItem = null;
        }
    }
}