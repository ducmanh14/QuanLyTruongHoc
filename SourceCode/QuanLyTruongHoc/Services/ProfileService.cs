using System;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class ProfileService
    {
        public Profile GetProfile(int userId, string vaiTro)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                if (vaiTro == "hs")
                {
                    string query = @"
                        SELECT u.id, u.ho_ten, u.email, u.vai_tro,
                               hs.ma_hs, ISNULL(lh.ten_lop, N'Chưa xếp lớp') AS ten_lop
                        FROM [USER] u
                        INNER JOIN HOC_SINH hs ON hs.user_id = u.id
                        LEFT JOIN LOP_HOC_SINH lhs 
                            ON lhs.hoc_sinh_id = hs.id 
                            AND lhs.trang_thai = 'dang_hoc'
                        LEFT JOIN LOP_HOC lh ON lhs.lop_id = lh.id
                        WHERE u.id = @UserId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Profile
                        {
                            UserId = Convert.ToInt32(reader["id"]),
                            HoTen = reader["ho_ten"].ToString(),
                            Email = reader["email"].ToString(),
                            VaiTro = reader["vai_tro"].ToString(),
                            MaHS = reader["ma_hs"].ToString(),
                            TenLop = reader["ten_lop"].ToString()
                        };
                    }
                }

                if (vaiTro == "gv")
                {
                    string query = @"
                        SELECT u.id, u.ho_ten, u.email, u.vai_tro,
                               gv.ma_gv, gv.chuyen_mon, gv.so_dien_thoai
                        FROM [USER] u
                        INNER JOIN GIAO_VIEN gv ON gv.user_id = u.id
                        WHERE u.id = @UserId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Profile
                        {
                            UserId = Convert.ToInt32(reader["id"]),
                            HoTen = reader["ho_ten"].ToString(),
                            Email = reader["email"].ToString(),
                            VaiTro = reader["vai_tro"].ToString(),
                            MaGV = reader["ma_gv"].ToString(),
                            ChuyenMon = reader["chuyen_mon"].ToString(),
                            SoDienThoai = reader["so_dien_thoai"].ToString()
                        };
                    }
                }

                string adminQuery = @"
                    SELECT id, ho_ten, email, vai_tro
                    FROM [USER]
                    WHERE id = @UserId";

                SqlCommand adminCmd = new SqlCommand(adminQuery, conn);
                adminCmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader adminReader = adminCmd.ExecuteReader();

                if (adminReader.Read())
                {
                    return new Profile
                    {
                        UserId = Convert.ToInt32(adminReader["id"]),
                        HoTen = adminReader["ho_ten"].ToString(),
                        Email = adminReader["email"].ToString(),
                        VaiTro = adminReader["vai_tro"].ToString()
                    };
                }
            }

            return null;
        }
    }
}
