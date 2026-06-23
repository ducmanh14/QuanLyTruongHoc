using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class LopHocService
    {
        public List<LopHoc> GetAll()
        {
            List<LopHoc> list = new List<LopHoc>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT id, gv_chu_nhiem_id, ten_lop, khoi, nien_khoa, si_so, ngay_tao
                    FROM LOP_HOC
                    ORDER BY id DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LopHoc
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        GvChuNhiemId = reader["gv_chu_nhiem_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["gv_chu_nhiem_id"]),
                        TenLop = reader["ten_lop"].ToString(),
                        Khoi = reader["khoi"].ToString(),
                        NienKhoa = reader["nien_khoa"].ToString(),
                        SiSo = Convert.ToInt32(reader["si_so"]),
                        NgayTao = Convert.ToDateTime(reader["ngay_tao"])
                    });
                }
            }

            return list;
        }

        public bool Add(LopHoc lop)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO LOP_HOC (gv_chu_nhiem_id, ten_lop, khoi, nien_khoa, si_so)
                    VALUES (@GvChuNhiemId, @TenLop, @Khoi, @NienKhoa, @SiSo)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GvChuNhiemId", lop.GvChuNhiemId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TenLop", lop.TenLop);
                cmd.Parameters.AddWithValue("@Khoi", lop.Khoi);
                cmd.Parameters.AddWithValue("@NienKhoa", lop.NienKhoa);
                cmd.Parameters.AddWithValue("@SiSo", lop.SiSo);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(LopHoc lop)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE LOP_HOC
                    SET gv_chu_nhiem_id = @GvChuNhiemId,
                        ten_lop = @TenLop,
                        khoi = @Khoi,
                        nien_khoa = @NienKhoa,
                        si_so = @SiSo
                    WHERE id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GvChuNhiemId", lop.GvChuNhiemId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TenLop", lop.TenLop);
                cmd.Parameters.AddWithValue("@Khoi", lop.Khoi);
                cmd.Parameters.AddWithValue("@NienKhoa", lop.NienKhoa);
                cmd.Parameters.AddWithValue("@SiSo", lop.SiSo);
                cmd.Parameters.AddWithValue("@Id", lop.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM LOP_HOC WHERE id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
