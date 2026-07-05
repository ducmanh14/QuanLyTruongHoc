using QuanLyTruongHoc.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc
{
    public partial class MainWindow : Window
    {
        private int _userId;
        private string _hoTen;
        private string _vaiTro;

        // Constructor nhận 3 tham số từ LoginWindow truyền sang
        public MainWindow(int userId, string hoTen, string vaiTro)
        {
            InitializeComponent();

            _userId = userId;
            _hoTen = hoTen;
            _vaiTro = vaiTro;

            txtWelcome.Text = "Xin chào, " + _hoTen;
            txtRole.Text = "Vai trò: " + _vaiTro;

            ApplyPermission();

            LoadDashboard();

            ShowDashboard();

            SetActiveButton(btnHome);
        }

        private void SetActiveButton(Button activeButton)
        {
            Button[] buttons =
            {
        btnHome,
        btnStudent,
        btnClass,
        btnStudentClass,
        btnTeacher,
        btnSubject,
        btnAssignment,
        btnAttendance,
        btnAttendanceHistory,
        btnGrade,
        btnProfile,
        btnChangePassword
    };

            foreach (Button btn in buttons)
            {
                if (btn == null) continue;

                btn.Background = Brushes.White;
                btn.Foreground = Brushes.Black;
            }

            activeButton.Background =
                new SolidColorBrush(Color.FromRgb(56, 189, 248));

            activeButton.Foreground = Brushes.White;
        }

        private void ShowDashboard()
        {
            DashboardPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Collapsed;
            MainFrame.Content = null;
        }

        private void ShowPage(Page page)
        {
            DashboardPanel.Visibility = Visibility.Collapsed;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(page);
        }

        private void LoadDashboard()
        {
            try
            {
                txtHocSinhCount.Text = new HocSinhService().GetAll().Count.ToString();
                txtGiaoVienCount.Text = new GiaoVienService().GetAll().Count.ToString();
                txtLopHocCount.Text = new LopHocService().GetAll().Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải Dashboard!\n" + ex.Message);
            }
        }
        // Logic phân quyền hiển thị menu
        private void ApplyPermission()
        {
            if (_vaiTro == "admin")
            {
                return;
            }

            if (_vaiTro == "gv")
            {
                btnClass.Visibility = Visibility.Collapsed;
                btnStudentClass.Visibility = Visibility.Collapsed;

                btnTeacher.Visibility = Visibility.Collapsed;
                btnSubject.Visibility = Visibility.Collapsed;
                btnAssignment.Visibility = Visibility.Collapsed;
            }

            if (_vaiTro == "hs")
            {
                btnStudent.Visibility = Visibility.Collapsed;
                btnClass.Visibility = Visibility.Collapsed;
                btnStudentClass.Visibility = Visibility.Collapsed;

                btnTeacher.Visibility = Visibility.Collapsed;
                btnSubject.Visibility = Visibility.Collapsed;
                btnAssignment.Visibility = Visibility.Collapsed;

                btnProfile.Visibility = Visibility.Visible;
                btnGrade.Visibility = Visibility.Visible;
                btnAttendance.Visibility = Visibility.Visible;

                btnGrade.Content = "Xem điểm";
                btnAttendance.Content = "Nhập mã điểm danh";
            }
        }


        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboard();

            ShowDashboard();

            SetActiveButton(btnHome);
        }


        private void BtnAttendance_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new AttendancePage(_userId, _vaiTro));

            SetActiveButton(btnAttendance);
        }

        private void BtnAttendanceHistory_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new AttendanceHistoryPage(_userId, _vaiTro));

            SetActiveButton(btnAttendanceHistory);
        }

        private void BtnGrade_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new GradeManagementPage(_userId, _vaiTro));

            SetActiveButton(btnGrade);
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new ProfilePage(_userId, _vaiTro));

            SetActiveButton(btnProfile);
        }

        // CÁC HÀM ĐIỀU HƯỚNG CŨ CỦA DỰ ÁN
        private void BtnTeacher_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new TeacherManagementPage());

            SetActiveButton(btnTeacher);
        }

        private void BtnSubject_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new SubjectManagementPage());

            SetActiveButton(btnSubject);
        }

        private void BtnAssignment_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new TeachingAssignmentPage());

            SetActiveButton(btnAssignment);
        }

        private void BtnStudent_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new StudentManagementPage(_vaiTro));

            SetActiveButton(btnStudent);
        }

        private void BtnClass_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new ClassManagementPage());

            SetActiveButton(btnClass);
        }

        private void BtnStudentClass_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(new StudentClassPage());

            SetActiveButton(btnStudentClass);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginWindow login = new LoginWindow();
                login.Show();

                this.Close();
            }
        }
    }
}