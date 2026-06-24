using System.Data.SqlClient;

namespace QuanLyTruongHoc.Data
{
    public class DatabaseHelper
    {
        // Đã thêm chữ DB vào sau QuanLyTruongHoc
        private static string connectionString = @"Server=.\SQLEXPRESS;Database=QuanLyTruongHocDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}