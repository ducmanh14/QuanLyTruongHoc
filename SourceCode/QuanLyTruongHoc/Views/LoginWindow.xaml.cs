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

            var user = userService.Login(email, password);

            if (user != null)
            {
                MainWindow mainWindow = new MainWindow(user.HoTen, user.VaiTro);
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