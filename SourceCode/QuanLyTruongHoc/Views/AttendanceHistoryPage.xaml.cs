using System.Windows;
using System.Windows.Controls;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class AttendanceHistoryPage : Page
    {
        private DiemDanhService service = new DiemDanhService();

        public AttendanceHistoryPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgHistory.ItemsSource = service.GetAll();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            dgHistory.ItemsSource = string.IsNullOrEmpty(keyword)
                ? service.GetAll()
                : service.SearchByStudentOrClass(keyword);
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }
    }
}
