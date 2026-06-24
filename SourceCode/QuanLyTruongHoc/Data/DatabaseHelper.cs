using System.Data.SqlClient;

namespace QuanLyTruongHoc.Data
{
    public class DatabaseHelper
    {
        
        private static string connectionString = @"Server=.\SQLEXPRESS;Database=QuanLyTruongHocDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}