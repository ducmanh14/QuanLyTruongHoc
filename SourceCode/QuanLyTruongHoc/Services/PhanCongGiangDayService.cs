using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class PhanCongGiangDayService
    {
        public List<PhanCongGiangDay> GetAll()
        {
            List<PhanCongGiangDay> list = new List<PhanCongGiangDay>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT pc.id, pc.lop_id, pc.giao_vien_id, pc.mon_hoc_id,
                           pc.hoc_ky, pc.nam_hoc,
                           lh.ten_lop, u.ho_ten AS ten_giao_vien, mh.ten_mon              
                    FROM PHAN_CONG_GIANG_DAY pc
                    INNER JOIN LOP_HOC lh ON pc.lop_id = lh.id
                    INNER JOIN GIAO_VIEN gv ON pc.giao_vien_id = gv.id
                    INNER JOIN [USER] u ON gv.user_id = u.id           
                    INNER JOIN MON_HOC mh ON pc.mon_hoc_id = mh.id
                    ORDER BY pc.id DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PhanCongGiangDay
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        LopId = Convert.ToInt32(reader["lop_id"]),
                        GiaoVienId = Convert.ToInt32(reader["giao_vien_id"]),
                        MonHocId = Convert.ToInt32(reader["mon_hoc_id"]),
                        HocKy = Convert.ToInt32(reader["hoc_ky"]),
                        NamHoc = reader["nam_hoc"].ToString(),
                        TenLop = reader["ten_lop"].ToString(),
                        TenGiaoVien = reader["ten_giao_vien"].ToString(),
                        TenMon = reader["ten_mon"].ToString()
                    });
                }
            }
            return list;
        }

        public bool Add(PhanCongGiangDay pc)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    INSERT INTO PHAN_CONG_GIANG_DAY
                    (lop_id, giao_vien_id, mon_hoc_id, hoc_ky, nam_hoc)
                    VALUES
                    (@LopId, @GiaoVienId, @MonHocId, @HocKy, @NamHoc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LopId", pc.LopId);
                cmd.Parameters.AddWithValue("@GiaoVienId", pc.GiaoVienId);
                cmd.Parameters.AddWithValue("@MonHocId", pc.MonHocId);
                cmd.Parameters.AddWithValue("@HocKy", pc.HocKy);
                cmd.Parameters.AddWithValue("@NamHoc", pc.NamHoc);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(PhanCongGiangDay pc)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    UPDATE PHAN_CONG_GIANG_DAY
                    SET lop_id = @LopId, giao_vien_id = @GiaoVienId, mon_hoc_id = @MonHocId, 
                        hoc_ky = @HocKy, nam_hoc = @NamHoc
                    WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LopId", pc.LopId);
                cmd.Parameters.AddWithValue("@GiaoVienId", pc.GiaoVienId);
                cmd.Parameters.AddWithValue("@MonHocId", pc.MonHocId);
                cmd.Parameters.AddWithValue("@HocKy", pc.HocKy);
                cmd.Parameters.AddWithValue("@NamHoc", pc.NamHoc);
                cmd.Parameters.AddWithValue("@Id", pc.Id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM PHAN_CONG_GIANG_DAY WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}