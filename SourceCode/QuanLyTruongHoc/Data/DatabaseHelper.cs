using System.Data.SqlClient;

namespace QuanLyTruongHoc.Data
{
    public class DatabaseHelper
    {
        // Đã thêm chữ DB vào sau QuanLyTruongHoc
        private static string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyTruongHocDB;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}