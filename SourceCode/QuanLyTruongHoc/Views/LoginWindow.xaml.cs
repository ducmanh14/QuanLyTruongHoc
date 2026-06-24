using System;
using System.Windows;
using QuanLyTruongHoc.Services;

namespace QuanLyTruongHoc.Views
{
    public partial class LoginWindow : Window
    {
        private UserService userService = new UserService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                txtMessage.Text = "Vui lòng nhập đầy đủ email và mật khẩu.";
                return;
            }

            // Gọi hàm đăng nhập chuẩn của hệ thống
            var user = userService.Login(email, password);

            if (user != null)
            {
                // SỬA DÒNG MỞ MAINWINDOW THEO ĐÚNG HƯỚNG DẪN 
                // Truyền đủ 3 tham số: Id, HoTen, VaiTro sang màn hình chính
                MainWindow mainWindow = new MainWindow(user.Id, user.HoTen, user.VaiTro);

                mainWindow.Show();
                this.Close();
            }
            else
            {
                txtMessage.Text = "Email hoặc mật khẩu không đúng.";
            }
        }
    }
}