using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class GiaoVienService
    {
        public List<GiaoVien> GetAll()
        {
            List<GiaoVien> list = new List<GiaoVien>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT gv.id, gv.user_id, gv.ma_gv, u.ho_ten, u.email,
                           gv.chuyen_mon, gv.so_dien_thoai, gv.ngay_tao
                    FROM GIAO_VIEN gv
                    INNER JOIN [USER] u ON gv.user_id = u.id
                    WHERE u.trang_thai = 1
                    ORDER BY gv.id DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new GiaoVien
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        UserId = Convert.ToInt32(reader["user_id"]),
                        MaGV = reader["ma_gv"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        Email = reader["email"].ToString(),
                        ChuyenMon = reader["chuyen_mon"].ToString(),
                        SoDienThoai = reader["so_dien_thoai"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["ngay_tao"])
                    });
                }
            }
            return list;
        }

        public List<GiaoVien> Search(string keyword)
        {
            List<GiaoVien> list = new List<GiaoVien>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT gv.id, gv.user_id, gv.ma_gv, u.ho_ten, u.email,
                           gv.chuyen_mon, gv.so_dien_thoai, gv.ngay_tao
                    FROM GIAO_VIEN gv
                    INNER JOIN [USER] u ON gv.user_id = u.id
                    WHERE u.trang_thai = 1
                    AND (
                        gv.ma_gv LIKE @Keyword
                        OR u.ho_ten LIKE @Keyword
                        OR u.email LIKE @Keyword
                        OR gv.chuyen_mon LIKE @Keyword
                    )
                    ORDER BY gv.id DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new GiaoVien
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        UserId = Convert.ToInt32(reader["user_id"]),
                        MaGV = reader["ma_gv"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        Email = reader["email"].ToString(),
                        ChuyenMon = reader["chuyen_mon"].ToString(),
                        SoDienThoai = reader["so_dien_thoai"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["ngay_tao"])
                    });
                }
            }
            return list;
        }

        public bool Add(GiaoVien gv)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string insertUser = @"
                        INSERT INTO [USER] (ho_ten, email, mat_khau, vai_tro, trang_thai)
                        OUTPUT INSERTED.id
                        VALUES (@HoTen, @Email, '123456', 'gv', 1)";
                    SqlCommand cmdUser = new SqlCommand(insertUser, conn, trans);
                    cmdUser.Parameters.AddWithValue("@HoTen", gv.HoTen);
                    cmdUser.Parameters.AddWithValue("@Email", gv.Email);
                    int userId = Convert.ToInt32(cmdUser.ExecuteScalar());

                    string insertGV = @"
                        INSERT INTO GIAO_VIEN (user_id, ma_gv, chuyen_mon, so_dien_thoai)
                        VALUES (@UserId, @MaGV, @ChuyenMon, @SoDienThoai)";
                    SqlCommand cmdGV = new SqlCommand(insertGV, conn, trans);
                    cmdGV.Parameters.AddWithValue("@UserId", userId);
                    cmdGV.Parameters.AddWithValue("@MaGV", gv.MaGV);
                    cmdGV.Parameters.AddWithValue("@ChuyenMon", gv.ChuyenMon ?? "");
                    cmdGV.Parameters.AddWithValue("@SoDienThoai", gv.SoDienThoai ?? "");
                    cmdGV.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public bool Update(GiaoVien gv)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string updateUser = @"
                        UPDATE [USER]
                        SET ho_ten = @HoTen, email = @Email, cap_nhat = GETDATE()
                        WHERE id = @UserId";
                    SqlCommand cmdUser = new SqlCommand(updateUser, conn, trans);
                    cmdUser.Parameters.AddWithValue("@HoTen", gv.HoTen);
                    cmdUser.Parameters.AddWithValue("@Email", gv.Email);
                    cmdUser.Parameters.AddWithValue("@UserId", gv.UserId);
                    cmdUser.ExecuteNonQuery();

                    string updateGV = @"
                        UPDATE GIAO_VIEN
                        SET ma_gv = @MaGV, chuyen_mon = @ChuyenMon, so_dien_thoai = @SoDienThoai
                        WHERE id = @Id";
                    SqlCommand cmdGV = new SqlCommand(updateGV, conn, trans);
                    cmdGV.Parameters.AddWithValue("@MaGV", gv.MaGV);
                    cmdGV.Parameters.AddWithValue("@ChuyenMon", gv.ChuyenMon ?? "");
                    cmdGV.Parameters.AddWithValue("@SoDienThoai", gv.SoDienThoai ?? "");
                    cmdGV.Parameters.AddWithValue("@Id", gv.Id);
                    cmdGV.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public bool Delete(int userId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    UPDATE [USER]
                    SET trang_thai = 0, cap_nhat = GETDATE()
                    WHERE id = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}