using System;
using System.Windows;
using QuanLyTruongHoc.Views;

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
                btnTeacher.Visibility = Visibility.Collapsed;
                btnSubject.Visibility = Visibility.Collapsed;
                btnAssignment.Visibility = Visibility.Collapsed;
            }

            if (_vaiTro == "hs")
            {
                btnStudent.Visibility = Visibility.Collapsed;
                btnClass.Visibility = Visibility.Collapsed;
                btnTeacher.Visibility = Visibility.Collapsed;
                btnSubject.Visibility = Visibility.Collapsed;
                btnAssignment.Visibility = Visibility.Collapsed;
                btnGrade.Content = "Xem điểm";
                btnAttendance.Content = "Nhập mã điểm danh";
            }
        }

        
        private void BtnAttendance_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AttendancePage());
        }

        private void BtnAttendanceHistory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AttendanceHistoryPage());
        }

        private void BtnGrade_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GradeManagementPage());
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage(_userId, _vaiTro));
        }

        // CÁC HÀM ĐIỀU HƯỚNG CŨ CỦA DỰ ÁN
        private void BtnTeacher_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TeacherManagementPage());
        }

        private void BtnSubject_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SubjectManagementPage());
        }

        private void BtnAssignment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TeachingAssignmentPage());
        }

        private void BtnStudent_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StudentManagementPage());
        }

        private void BtnClass_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClassManagementPage());
        }

        private void BtnStudentClass_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StudentClassPage());
        }
    }
}