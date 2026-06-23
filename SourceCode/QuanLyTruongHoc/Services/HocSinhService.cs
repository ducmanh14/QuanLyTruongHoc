using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class HocSinhService
    {
        public List<HocSinh> GetAll()
        {
            List<HocSinh> list = new List<HocSinh>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT hs.id, hs.user_id, hs.ma_hs, u.ho_ten, u.email,
                           hs.ngay_sinh, hs.gioi_tinh, hs.dia_chi,
                           ISNULL(lh.ten_lop, N'Chưa xếp lớp') AS ten_lop,
                           ISNULL(lhs.trang_thai, N'Chưa xếp lớp') AS trang_thai
                    FROM HOC_SINH hs
                    INNER JOIN [USER] u ON hs.user_id = u.id
                    LEFT JOIN LOP_HOC_SINH lhs 
                        ON lhs.hoc_sinh_id = hs.id 
                        AND lhs.trang_thai = 'dang_hoc'
                    LEFT JOIN LOP_HOC lh ON lhs.lop_id = lh.id
                    WHERE u.trang_thai = 1
                    ORDER BY hs.id DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new HocSinh
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        UserId = Convert.ToInt32(reader["user_id"]),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        Email = reader["email"].ToString(),
                        NgaySinh = reader["ngay_sinh"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["ngay_sinh"]),
                        GioiTinh = reader["gioi_tinh"].ToString(),
                        DiaChi = reader["dia_chi"].ToString(),
                        TenLop = reader["ten_lop"].ToString(),
                        TrangThai = reader["trang_thai"].ToString()
                    });
                }
            }

            return list;
        }

        public List<HocSinh> Search(string keyword)
        {
            List<HocSinh> list = new List<HocSinh>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT hs.id, hs.user_id, hs.ma_hs, u.ho_ten, u.email,
                           hs.ngay_sinh, hs.gioi_tinh, hs.dia_chi,
                           ISNULL(lh.ten_lop, N'Chưa xếp lớp') AS ten_lop,
                           ISNULL(lhs.trang_thai, N'Chưa xếp lớp') AS trang_thai
                    FROM HOC_SINH hs
                    INNER JOIN [USER] u ON hs.user_id = u.id
                    LEFT JOIN LOP_HOC_SINH lhs 
                        ON lhs.hoc_sinh_id = hs.id 
                        AND lhs.trang_thai = 'dang_hoc'
                    LEFT JOIN LOP_HOC lh ON lhs.lop_id = lh.id
                    WHERE u.trang_thai = 1
                    AND (
                        hs.ma_hs LIKE @Keyword 
                        OR u.ho_ten LIKE @Keyword 
                        OR u.email LIKE @Keyword
                        OR lh.ten_lop LIKE @Keyword
                    )
                    ORDER BY hs.id DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new HocSinh
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        UserId = Convert.ToInt32(reader["user_id"]),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        Email = reader["email"].ToString(),
                        NgaySinh = reader["ngay_sinh"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["ngay_sinh"]),
                        GioiTinh = reader["gioi_tinh"].ToString(),
                        DiaChi = reader["dia_chi"].ToString(),
                        TenLop = reader["ten_lop"].ToString(),
                        TrangThai = reader["trang_thai"].ToString()
                    });
                }
            }

            return list;
        }

        public bool Add(HocSinh hs)
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
                        VALUES (@HoTen, @Email, '123456', 'hs', 1)";

                    SqlCommand cmdUser = new SqlCommand(insertUser, conn, trans);
                    cmdUser.Parameters.AddWithValue("@HoTen", hs.HoTen);
                    cmdUser.Parameters.AddWithValue("@Email", hs.Email);

                    int userId = Convert.ToInt32(cmdUser.ExecuteScalar());

                    string insertHS = @"
                        INSERT INTO HOC_SINH (user_id, ma_hs, ngay_sinh, gioi_tinh, dia_chi)
                        VALUES (@UserId, @MaHS, @NgaySinh, @GioiTinh, @DiaChi)";

                    SqlCommand cmdHS = new SqlCommand(insertHS, conn, trans);
                    cmdHS.Parameters.AddWithValue("@UserId", userId);
                    cmdHS.Parameters.AddWithValue("@MaHS", hs.MaHS);
                    cmdHS.Parameters.AddWithValue("@NgaySinh", hs.NgaySinh ?? (object)DBNull.Value);
                    cmdHS.Parameters.AddWithValue("@GioiTinh", hs.GioiTinh ?? "");
                    cmdHS.Parameters.AddWithValue("@DiaChi", hs.DiaChi ?? "");

                    cmdHS.ExecuteNonQuery();

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

        public bool Update(HocSinh hs)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string updateUser = @"
                        UPDATE [USER]
                        SET ho_ten = @HoTen,
                            email = @Email,
                            cap_nhat = GETDATE()
                        WHERE id = @UserId";

                    SqlCommand cmdUser = new SqlCommand(updateUser, conn, trans);
                    cmdUser.Parameters.AddWithValue("@HoTen", hs.HoTen);
                    cmdUser.Parameters.AddWithValue("@Email", hs.Email);
                    cmdUser.Parameters.AddWithValue("@UserId", hs.UserId);
                    cmdUser.ExecuteNonQuery();

                    string updateHS = @"
                        UPDATE HOC_SINH
                        SET ma_hs = @MaHS,
                            ngay_sinh = @NgaySinh,
                            gioi_tinh = @GioiTinh,
                            dia_chi = @DiaChi
                        WHERE id = @Id";

                    SqlCommand cmdHS = new SqlCommand(updateHS, conn, trans);
                    cmdHS.Parameters.AddWithValue("@MaHS", hs.MaHS);
                    cmdHS.Parameters.AddWithValue("@NgaySinh", hs.NgaySinh ?? (object)DBNull.Value);
                    cmdHS.Parameters.AddWithValue("@GioiTinh", hs.GioiTinh ?? "");
                    cmdHS.Parameters.AddWithValue("@DiaChi", hs.DiaChi ?? "");
                    cmdHS.Parameters.AddWithValue("@Id", hs.Id);

                    cmdHS.ExecuteNonQuery();

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
                    SET trang_thai = 0,
                        cap_nhat = GETDATE()
                    WHERE id = @UserId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
