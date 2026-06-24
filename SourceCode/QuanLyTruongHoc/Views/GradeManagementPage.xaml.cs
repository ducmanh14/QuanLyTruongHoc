using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class GradeManagementPage : Page
    {
        private DiemService service = new DiemService();
        private Diem selectedDiem = null;

        public GradeManagementPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgGrades.ItemsSource = service.GetAll();
        }

        private Diem GetFormData()
        {
            int hocSinhId, lopId, monHocId, hocKy;
            decimal giaTri;

            if (!int.TryParse(txtHocSinhId.Text.Trim(), out hocSinhId) ||
                !int.TryParse(txtLopId.Text.Trim(), out lopId) ||
                !int.TryParse(txtMonHocId.Text.Trim(), out monHocId) ||
                !int.TryParse(txtHocKy.Text.Trim(), out hocKy) ||
                !decimal.TryParse(txtGiaTri.Text.Trim(), out giaTri))
            {
                MessageBox.Show("ID học sinh, ID lớp, ID môn, học kỳ và điểm phải đúng định dạng số.");
                return null;
            }

            if (cbLoaiDiem.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại điểm.");
                return null;
            }

            return new Diem
            {
                HocSinhId = hocSinhId,
                LopId = lopId,
                MonHocId = monHocId,
                GiaoVienId = null,
                LoaiDiem = ((ComboBoxItem)cbLoaiDiem.SelectedItem).Content.ToString(),
                GiaTri = giaTri,
                HocKy = hocKy,
                NamHoc = txtNamHoc.Text.Trim()
            };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Diem diem = GetFormData();
            if (diem == null) return;

            if (service.Add(diem))
            {
                MessageBox.Show("Thêm điểm thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Thêm điểm thất bại.");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDiem == null)
            {
                MessageBox.Show("Vui lòng chọn điểm cần sửa.");
                return;
            }

            Diem diem = GetFormData();
            if (diem == null) return;

            diem.Id = selectedDiem.Id;

            if (service.Update(diem))
            {
                MessageBox.Show("Cập nhật điểm thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDiem == null)
            {
                MessageBox.Show("Vui lòng chọn điểm cần xóa.");
                return;
            }

            if (service.Delete(selectedDiem.Id))
            {
                MessageBox.Show("Xóa điểm thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Xóa thất bại.");
            }
        }

        private void DgGrades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDiem = dgGrades.SelectedItem as Diem;

            if (selectedDiem != null)
            {
                txtHocSinhId.Text = selectedDiem.HocSinhId.ToString();
                txtLopId.Text = selectedDiem.LopId.ToString();
                txtMonHocId.Text = selectedDiem.MonHocId.ToString();
                txtHocKy.Text = selectedDiem.HocKy.ToString();
                txtNamHoc.Text = selectedDiem.NamHoc;
                txtGiaTri.Text = selectedDiem.GiaTri.ToString();

                foreach (ComboBoxItem item in cbLoaiDiem.Items)
                {
                    if (item.Content.ToString() == selectedDiem.LoaiDiem)
                    {
                        cbLoaiDiem.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }
}
