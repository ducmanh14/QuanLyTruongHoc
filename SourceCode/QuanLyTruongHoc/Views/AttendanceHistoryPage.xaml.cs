using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class AttendanceHistoryPage : Page
    {
        private DiemDanhService service = new DiemDanhService();

        private int _userId;
        private string _vaiTro;

        public AttendanceHistoryPage(int userId, string vaiTro)
        {
            InitializeComponent();

            _userId = userId;
            _vaiTro = vaiTro;

            LoadData();
        }

        private void LoadData()
        {
            if (_vaiTro == "hs")
            {
                dgHistory.ItemsSource = service.GetByStudentUserId(_userId);
            }
            else
            {
                dgHistory.ItemsSource = service.GetAll();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (_vaiTro == "hs")
            {
                // Học sinh không được tìm kiếm người khác
                dgHistory.ItemsSource = service.GetByStudentUserId(_userId);
            }
            else
            {
                dgHistory.ItemsSource = string.IsNullOrEmpty(keyword)
                    ? service.GetAll()
                    : service.SearchByStudentOrClass(keyword);
            }
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }
    }
}