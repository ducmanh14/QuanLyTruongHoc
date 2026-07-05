using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class DiemDanhService
    {
        public string TaoMaDiemDanh()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
        }

        public List<DiemDanh> GetAll()
        {
            List<DiemDanh> list = new List<DiemDanh>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT dd.id, dd.lop_id, dd.hoc_sinh_id, dd.giao_vien_id,
                           lh.ten_lop, hs.ma_hs, u.ho_ten,
                           dd.ngay_diem_danh, dd.buoi_hoc, dd.trang_thai,
                           dd.ghi_chu, dd.ngay_tao
                    FROM DIEM_DANH dd
                    INNER JOIN LOP_HOC lh ON dd.lop_id = lh.id
                    INNER JOIN HOC_SINH hs ON dd.hoc_sinh_id = hs.id
                    INNER JOIN [USER] u ON hs.user_id = u.id
                    ORDER BY dd.ngay_tao DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DiemDanh
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        LopId = Convert.ToInt32(reader["lop_id"]),
                        HocSinhId = Convert.ToInt32(reader["hoc_sinh_id"]),
                        GiaoVienId = reader["giao_vien_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["giao_vien_id"]),
                        TenLop = reader["ten_lop"].ToString(),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        NgayDiemDanh = Convert.ToDateTime(reader["ngay_diem_danh"]),
                        BuoiHoc = reader["buoi_hoc"].ToString(),
                        TrangThai = reader["trang_thai"].ToString(),
                        GhiChu = reader["ghi_chu"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["ngay_tao"])
                    });
                }
            }

            return list;
        }

        public List<DiemDanh> GetAttendanceByStudent(int userId)
        {
            List<DiemDanh> list = new List<DiemDanh>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
        SELECT dd.id,
               dd.lop_id,
               dd.hoc_sinh_id,
               dd.giao_vien_id,
               lh.ten_lop,
               hs.ma_hs,
               u.ho_ten,
               dd.ngay_diem_danh,
               dd.buoi_hoc,
               dd.trang_thai,
               dd.ghi_chu,
               dd.ngay_tao
        FROM DIEM_DANH dd
        INNER JOIN HOC_SINH hs
            ON dd.hoc_sinh_id = hs.id
        INNER JOIN [USER] u
            ON hs.user_id = u.id
        INNER JOIN LOP_HOC lh
            ON dd.lop_id = lh.id
        WHERE hs.user_id = @UserId
        ORDER BY dd.ngay_tao DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DiemDanh
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        LopId = Convert.ToInt32(reader["lop_id"]),
                        HocSinhId = Convert.ToInt32(reader["hoc_sinh_id"]),
                        GiaoVienId = reader["giao_vien_id"] == DBNull.Value
                            ? null
                            : (int?)Convert.ToInt32(reader["giao_vien_id"]),
                        TenLop = reader["ten_lop"].ToString(),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        NgayDiemDanh = Convert.ToDateTime(reader["ngay_diem_danh"]),
                        BuoiHoc = reader["buoi_hoc"].ToString(),
                        TrangThai = reader["trang_thai"].ToString(),
                        GhiChu = reader["ghi_chu"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["ngay_tao"])
                    });
                }
            }

            return list;
        }

        public List<DiemDanh> GetByStudentUserId(int userId)
        {
            List<DiemDanh> list = new List<DiemDanh>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
            SELECT dd.id, dd.lop_id, dd.hoc_sinh_id, dd.giao_vien_id,
                   lh.ten_lop, hs.ma_hs, u.ho_ten,
                   dd.ngay_diem_danh, dd.buoi_hoc, dd.trang_thai,
                   dd.ghi_chu, dd.ngay_tao
            FROM DIEM_DANH dd
            INNER JOIN HOC_SINH hs ON dd.hoc_sinh_id = hs.id
            INNER JOIN [USER] u ON hs.user_id = u.id
            INNER JOIN LOP_HOC lh ON dd.lop_id = lh.id
            WHERE hs.user_id = @UserId
            ORDER BY dd.ngay_tao DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DiemDanh
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        LopId = Convert.ToInt32(reader["lop_id"]),
                        HocSinhId = Convert.ToInt32(reader["hoc_sinh_id"]),
                        GiaoVienId = reader["giao_vien_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["giao_vien_id"]),
                        TenLop = reader["ten_lop"].ToString(),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        NgayDiemDanh = Convert.ToDateTime(reader["ngay_diem_danh"]),
                        BuoiHoc = reader["buoi_hoc"].ToString(),
                        TrangThai = reader["trang_thai"].ToString(),
                        GhiChu = reader["ghi_chu"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["ngay_tao"])
                    });
                }
            }

            return list;
        }
        public bool DiemDanhThuCong(DiemDanh dd)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO DIEM_DANH
                    (lop_id, hoc_sinh_id, giao_vien_id, ngay_diem_danh, buoi_hoc, trang_thai, ghi_chu, ngay_tao)
                    VALUES
                    (@LopId, @HocSinhId, @GiaoVienId, @NgayDiemDanh, @BuoiHoc, @TrangThai, @GhiChu, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LopId", dd.LopId);
                cmd.Parameters.AddWithValue("@HocSinhId", dd.HocSinhId);
                cmd.Parameters.AddWithValue("@GiaoVienId", dd.GiaoVienId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDiemDanh", dd.NgayDiemDanh);
                cmd.Parameters.AddWithValue("@BuoiHoc", dd.BuoiHoc);
                cmd.Parameters.AddWithValue("@TrangThai", dd.TrangThai);
                cmd.Parameters.AddWithValue("@GhiChu", dd.GhiChu ?? "");

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<DiemDanh> SearchByStudentOrClass(string keyword)
        {
            List<DiemDanh> list = new List<DiemDanh>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT dd.id, dd.lop_id, dd.hoc_sinh_id, dd.giao_vien_id,
                           lh.ten_lop, hs.ma_hs, u.ho_ten,
                           dd.ngay_diem_danh, dd.buoi_hoc, dd.trang_thai,
                           dd.ghi_chu, dd.ngay_tao
                    FROM DIEM_DANH dd
                    INNER JOIN LOP_HOC lh ON dd.lop_id = lh.id
                    INNER JOIN HOC_SINH hs ON dd.hoc_sinh_id = hs.id
                    INNER JOIN [USER] u ON hs.user_id = u.id
                    WHERE hs.ma_hs LIKE @Keyword
                       OR u.ho_ten LIKE @Keyword
                       OR lh.ten_lop LIKE @Keyword
                    ORDER BY dd.ngay_tao DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DiemDanh
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        LopId = Convert.ToInt32(reader["lop_id"]),
                        HocSinhId = Convert.ToInt32(reader["hoc_sinh_id"]),
                        GiaoVienId = reader["giao_vien_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["giao_vien_id"]),
                        TenLop = reader["ten_lop"].ToString(),
                        MaHS = reader["ma_hs"].ToString(),
                        HoTen = reader["ho_ten"].ToString(),
                        NgayDiemDanh = Convert.ToDateTime(reader["ngay_diem_danh"]),
                        BuoiHoc = reader["buoi_hoc"].ToString(),
                        TrangThai = reader["trang_thai"].ToString(),
                        GhiChu = reader["ghi_chu"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["ngay_tao"])
                    });
                }
            }

            return list;
        }
    }
}
