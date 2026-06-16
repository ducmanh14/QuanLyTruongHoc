using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTruongHoc.Data;
using QuanLyTruongHoc.Models;

namespace QuanLyTruongHoc.Services
{
    public class MonHocService
    {
        public List<MonHoc> GetAll()
        {
            List<MonHoc> list = new List<MonHoc>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, ma_mon, ten_mon, so_tiet FROM MON_HOC ORDER BY id DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new MonHoc
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        MaMon = reader["ma_mon"].ToString(),
                        TenMon = reader["ten_mon"].ToString(),
                        SoTiet = Convert.ToInt32(reader["so_tiet"])
                    });
                }
            }
            return list;
        }

        public bool Add(MonHoc mh)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    INSERT INTO MON_HOC (ma_mon, ten_mon, so_tiet)
                    VALUES (@MaMon, @TenMon, @SoTiet)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaMon", mh.MaMon);
                cmd.Parameters.AddWithValue("@TenMon", mh.TenMon);
                cmd.Parameters.AddWithValue("@SoTiet", mh.SoTiet);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(MonHoc mh)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    UPDATE MON_HOC
                    SET ma_mon = @MaMon, ten_mon = @TenMon, so_tiet = @SoTiet 
                    WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaMon", mh.MaMon);
                cmd.Parameters.AddWithValue("@TenMon", mh.TenMon);
                cmd.Parameters.AddWithValue("@SoTiet", mh.SoTiet);
                cmd.Parameters.AddWithValue("@Id", mh.Id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM MON_HOC WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}