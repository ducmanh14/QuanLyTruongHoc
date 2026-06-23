using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class LopHocSinhService
    {
        public List<LopHocSinh> GetAll()
        {
            List<LopHocSinh> list = new List<LopHocSinh>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT lhs.id, lhs.lop_id, lhs.hoc_sinh_id,
                           lh.ten_lop, hs.ma_hs, u.ho_ten,
                           lhs.trang_thai, lhs.ngay_vao_lop, lhs.ngay_roi_lop
                    FROM LOP_HOC_SINH lhs
                    INNER JOIN LOP_HOC lh ON lhs.lop_id = lh.id
                    INNER JOIN HOC_SINH hs ON lhs.hoc_sinh_id = hs.id
                    INNER JOIN [USER] u ON hs.user_id = u.id
                    WHERE u.trang_thai = 1
                    ORDER BY lhs.id DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LopHocSinh
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        LopId = Convert.ToInt32(reader["lop_id"]),
                        HocSinhId = Convert.ToInt32(reader["hoc_sinh_id"]),
                        TenLop = reader["ten_lop"].ToString(),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        TrangThai = reader["trang_thai"].ToString(),
                        NgayVaoLop = Convert.ToDateTime(reader["ngay_vao_lop"]),
                        NgayRoiLop = reader["ngay_roi_lop"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["ngay_roi_lop"])
                    });
                }
            }

            return list;
        }

        public bool AddStudentToClass(int lopId, int hocSinhId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string closeOld = @"
                        UPDATE LOP_HOC_SINH
                        SET trang_thai = 'nghi',
                            ngay_roi_lop = GETDATE()
                        WHERE hoc_sinh_id = @HocSinhId
                        AND trang_thai = 'dang_hoc'";

                    SqlCommand cmdClose = new SqlCommand(closeOld, conn, trans);
                    cmdClose.Parameters.AddWithValue("@HocSinhId", hocSinhId);
                    cmdClose.ExecuteNonQuery();

                    string insertNew = @"
                        INSERT INTO LOP_HOC_SINH (lop_id, hoc_sinh_id, trang_thai, ngay_vao_lop)
                        VALUES (@LopId, @HocSinhId, 'dang_hoc', GETDATE())";

                    SqlCommand cmdInsert = new SqlCommand(insertNew, conn, trans);
                    cmdInsert.Parameters.AddWithValue("@LopId", lopId);
                    cmdInsert.Parameters.AddWithValue("@HocSinhId", hocSinhId);
                    cmdInsert.ExecuteNonQuery();

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

        public bool ChangeStatus(int id, string trangThai)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE LOP_HOC_SINH
                    SET trang_thai = @TrangThai,
                        ngay_roi_lop = CASE WHEN @TrangThai = 'nghi' THEN GETDATE() ELSE NULL END
                    WHERE id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
