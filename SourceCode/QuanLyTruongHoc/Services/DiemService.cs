using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class DiemService
    {
        public List<Diem> GetAll()
        {
            List<Diem> list = new List<Diem>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT d.id, d.hoc_sinh_id, d.lop_id, d.mon_hoc_id, d.giao_vien_id,
                           hs.ma_hs, u.ho_ten, lh.ten_lop, mh.ten_mon,
                           d.loai_diem, d.gia_tri, d.hoc_ky, d.nam_hoc, d.ngay_nhap
                    FROM DIEM d
                    INNER JOIN HOC_SINH hs ON d.hoc_sinh_id = hs.id
                    INNER JOIN [USER] u ON hs.user_id = u.id
                    INNER JOIN LOP_HOC lh ON d.lop_id = lh.id
                    INNER JOIN MON_HOC mh ON d.mon_hoc_id = mh.id
                    ORDER BY d.ngay_nhap DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Diem
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        HocSinhId = Convert.ToInt32(reader["hoc_sinh_id"]),
                        LopId = Convert.ToInt32(reader["lop_id"]),
                        MonHocId = Convert.ToInt32(reader["mon_hoc_id"]),
                        GiaoVienId = reader["giao_vien_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["giao_vien_id"]),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        TenLop = reader["ten_lop"].ToString(),
                        TenMon = reader["ten_mon"].ToString(),
                        LoaiDiem = reader["loai_diem"].ToString(),
                        GiaTri = Convert.ToDecimal(reader["gia_tri"]),
                        HocKy = Convert.ToInt32(reader["hoc_ky"]),
                        NamHoc = reader["nam_hoc"].ToString(),
                        NgayNhap = Convert.ToDateTime(reader["ngay_nhap"])
                    });
                }
            }

            return list;
        }

        public bool Add(Diem diem)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO DIEM
                    (hoc_sinh_id, lop_id, mon_hoc_id, giao_vien_id, loai_diem, gia_tri, hoc_ky, nam_hoc, ngay_nhap)
                    VALUES
                    (@HocSinhId, @LopId, @MonHocId, @GiaoVienId, @LoaiDiem, @GiaTri, @HocKy, @NamHoc, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HocSinhId", diem.HocSinhId);
                cmd.Parameters.AddWithValue("@LopId", diem.LopId);
                cmd.Parameters.AddWithValue("@MonHocId", diem.MonHocId);
                cmd.Parameters.AddWithValue("@GiaoVienId", diem.GiaoVienId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiDiem", diem.LoaiDiem);
                cmd.Parameters.AddWithValue("@GiaTri", diem.GiaTri);
                cmd.Parameters.AddWithValue("@HocKy", diem.HocKy);
                cmd.Parameters.AddWithValue("@NamHoc", diem.NamHoc);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Diem diem)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE DIEM
                    SET hoc_sinh_id = @HocSinhId,
                        lop_id = @LopId,
                        mon_hoc_id = @MonHocId,
                        giao_vien_id = @GiaoVienId,
                        loai_diem = @LoaiDiem,
                        gia_tri = @GiaTri,
                        hoc_ky = @HocKy,
                        nam_hoc = @NamHoc
                    WHERE id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HocSinhId", diem.HocSinhId);
                cmd.Parameters.AddWithValue("@LopId", diem.LopId);
                cmd.Parameters.AddWithValue("@MonHocId", diem.MonHocId);
                cmd.Parameters.AddWithValue("@GiaoVienId", diem.GiaoVienId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiDiem", diem.LoaiDiem);
                cmd.Parameters.AddWithValue("@GiaTri", diem.GiaTri);
                cmd.Parameters.AddWithValue("@HocKy", diem.HocKy);
                cmd.Parameters.AddWithValue("@NamHoc", diem.NamHoc);
                cmd.Parameters.AddWithValue("@Id", diem.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM DIEM WHERE id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Diem> Search(string keyword)
        {
            List<Diem> list = new List<Diem>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT d.id, d.hoc_sinh_id, d.lop_id, d.mon_hoc_id, d.giao_vien_id,
                           hs.ma_hs, u.ho_ten, lh.ten_lop, mh.ten_mon,
                           d.loai_diem, d.gia_tri, d.hoc_ky, d.nam_hoc, d.ngay_nhap
                    FROM DIEM d
                    INNER JOIN HOC_SINH hs ON d.hoc_sinh_id = hs.id
                    INNER JOIN [USER] u ON hs.user_id = u.id
                    INNER JOIN LOP_HOC lh ON d.lop_id = lh.id
                    INNER JOIN MON_HOC mh ON d.mon_hoc_id = mh.id
                    WHERE hs.ma_hs LIKE @Keyword
                       OR u.ho_ten LIKE @Keyword
                       OR lh.ten_lop LIKE @Keyword
                       OR mh.ten_mon LIKE @Keyword
                    ORDER BY d.ngay_nhap DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Diem
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        HocSinhId = Convert.ToInt32(reader["hoc_sinh_id"]),
                        LopId = Convert.ToInt32(reader["lop_id"]),
                        MonHocId = Convert.ToInt32(reader["mon_hoc_id"]),
                        GiaoVienId = reader["giao_vien_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["giao_vien_id"]),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        TenLop = reader["ten_lop"].ToString(),
                        TenMon = reader["ten_mon"].ToString(),
                        LoaiDiem = reader["loai_diem"].ToString(),
                        GiaTri = Convert.ToDecimal(reader["gia_tri"]),
                        HocKy = Convert.ToInt32(reader["hoc_ky"]),
                        NamHoc = reader["nam_hoc"].ToString(),
                        NgayNhap = Convert.ToDateTime(reader["ngay_nhap"])
                    });
                }
            }

            return list;
        }
    }
}
