using System;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class UserService
    {
        public User Login(string email, string password)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT id, ho_ten, email, mat_khau, vai_tro, trang_thai
                    FROM [USER]
                    WHERE email = @Email 
                    AND mat_khau = @MatKhau
                    AND trang_thai = 1
                ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@MatKhau", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        HoTen = reader["ho_ten"].ToString(),
                        Email = reader["email"].ToString(),
                        MatKhau = reader["mat_khau"].ToString(),
                        VaiTro = reader["vai_tro"].ToString(),
                        TrangThai = Convert.ToBoolean(reader["trang_thai"])
                    };
                }

                return null;
            }
        }
    }
}