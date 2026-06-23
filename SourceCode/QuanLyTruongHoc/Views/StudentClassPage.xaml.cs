using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class StudentClassPage : Page
    {
        private LopHocSinhService service = new LopHocSinhService();
        private LopHocSinh selectedItem = null;

        public StudentClassPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgStudentClass.ItemsSource = service.GetAll();
        }

        private void BtnAddToClass_Click(object sender, RoutedEventArgs e)
        {
            int hocSinhId;
            int lopId;

            if (!int.TryParse(txtHocSinhId.Text.Trim(), out hocSinhId) ||
                !int.TryParse(txtLopId.Text.Trim(), out lopId))
            {
                MessageBox.Show("ID học sinh và ID lớp phải là số.");
                return;
            }

            if (service.AddStudentToClass(lopId, hocSinhId))
            {
                MessageBox.Show("Xếp học sinh vào lớp thành công!");
                LoadData();
                txtHocSinhId.Clear();
                txtLopId.Clear();
            }
            else
            {
                MessageBox.Show("Xếp lớp thất bại.");
            }
        }

        private void BtnChangeToNghi_Click(object sender, RoutedEventArgs e)
        {
            ChangeStatus("nghi");
        }

        private void BtnChangeToDangHoc_Click(object sender, RoutedEventArgs e)
        {
            ChangeStatus("dang_hoc");
        }

        private void ChangeStatus(string status)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng.");
                return;
            }

            if (service.ChangeStatus(selectedItem.Id, status))
            {
                MessageBox.Show("Cập nhật trạng thái thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Cập nhật trạng thái thất bại.");
            }
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void DgStudentClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = dgStudentClass.SelectedItem as LopHocSinh;

            if (selectedItem != null)
            {
                txtHocSinhId.Text = selectedItem.HocSinhId.ToString();
                txtLopId.Text = selectedItem.LopId.ToString();
            }
        }
    }
}
