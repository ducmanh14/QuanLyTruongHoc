using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTruongHoc.Models
{
    public class Profile
    {
        public int UserId { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string VaiTro { get; set; }

        public string MaHS { get; set; }
        public string MaGV { get; set; }
        public string TenLop { get; set; }
        public string ChuyenMon { get; set; }
        public string SoDienThoai { get; set; }
    }
}


