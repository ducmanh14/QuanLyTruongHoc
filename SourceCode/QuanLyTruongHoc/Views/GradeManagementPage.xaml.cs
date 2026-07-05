using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Models;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class GradeManagementPage : Page
    {
        private DiemService service = new DiemService();
        private HocSinhService hocSinhService = new HocSinhService();
        private LopHocService lopHocService = new LopHocService();
        private MonHocService monHocService = new MonHocService();

        private Diem selectedDiem = null;

        private int _userId;
        private string _vaiTro;

        public GradeManagementPage(int userId, string vaiTro)
        {
            InitializeComponent();

            _userId = userId;
            _vaiTro = vaiTro;

            LoadCombo();

            if (_vaiTro == "hs")
            {
                FormPanel.Visibility = Visibility.Collapsed;
            }

            LoadData();
        }

        private void LoadCombo()
        {
            cbHocSinh.ItemsSource = hocSinhService.GetAll();
            cbHocSinh.DisplayMemberPath = "HoTen";
            cbHocSinh.SelectedValuePath = "Id";

            cbLopHoc.ItemsSource = lopHocService.GetAll();
            cbLopHoc.DisplayMemberPath = "TenLop";
            cbLopHoc.SelectedValuePath = "Id";

            cbMonHoc.ItemsSource = monHocService.GetAll();
            cbMonHoc.DisplayMemberPath = "TenMon";
            cbMonHoc.SelectedValuePath = "Id";
        }

        private void LoadData()
        {
            if (_vaiTro == "hs")
            {
                dgGrades.ItemsSource = service.GetByStudentUserId(_userId);
            }
            else
            {
                dgGrades.ItemsSource = service.GetAll();
            }
        }

        private Diem GetFormData()
        {
            if (cbHocSinh.SelectedValue == null ||
                cbLopHoc.SelectedValue == null ||
                cbMonHoc.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh, lớp và môn học.");
                return null;
            }

            int hocKy;
            decimal giaTri;

            if (!int.TryParse(txtHocKy.Text.Trim(), out hocKy))
            {
                MessageBox.Show("Học kỳ phải là số.");
                return null;
            }

            if (!decimal.TryParse(txtGiaTri.Text.Trim(), out giaTri))
            {
                MessageBox.Show("Điểm không hợp lệ.");
                return null;
            }

            if (cbLoaiDiem.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại điểm.");
                return null;
            }

            return new Diem
            {
                HocSinhId = (int)cbHocSinh.SelectedValue,
                LopId = (int)cbLopHoc.SelectedValue,
                MonHocId = (int)cbMonHoc.SelectedValue,
                GiaoVienId = null,
                HocKy = hocKy,
                NamHoc = txtNamHoc.Text.Trim(),
                GiaTri = giaTri,
                LoaiDiem = ((ComboBoxItem)cbLoaiDiem.SelectedItem).Content.ToString()
            };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Diem diem = GetFormData();

            if (diem == null)
                return;

            if (service.Add(diem))
            {
                MessageBox.Show("Thêm điểm thành công!");
                LoadData();
                ClearForm();
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

            if (diem == null)
                return;

            diem.Id = selectedDiem.Id;

            if (service.Update(diem))
            {
                MessageBox.Show("Cập nhật thành công!");
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
            if (selectedDiem == null)
            {
                MessageBox.Show("Vui lòng chọn điểm cần xóa.");
                return;
            }

            if (service.Delete(selectedDiem.Id))
            {
                MessageBox.Show("Xóa thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Xóa thất bại.");
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
                LoadData();
            else
                dgGrades.ItemsSource = service.Search(keyword);
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            selectedDiem = null;

            cbHocSinh.SelectedIndex = -1;
            cbLopHoc.SelectedIndex = -1;
            cbMonHoc.SelectedIndex = -1;

            cbLoaiDiem.SelectedIndex = -1;

            txtHocKy.Clear();
            txtNamHoc.Clear();
            txtGiaTri.Clear();

            dgGrades.SelectedItem = null;
        }

        private void DgGrades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDiem = dgGrades.SelectedItem as Diem;

            if (selectedDiem == null)
                return;

            cbHocSinh.SelectedValue = selectedDiem.HocSinhId;
            cbLopHoc.SelectedValue = selectedDiem.LopId;
            cbMonHoc.SelectedValue = selectedDiem.MonHocId;

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