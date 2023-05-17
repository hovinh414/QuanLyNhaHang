using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHangE2.ViewModel
{
    public class NhanVienViewModel
    {
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string TenDangNhap { get; set; }
        public string GioiTinh { get; set; }
        public string ChucVu { get; set; }
        public string DiaChi { get; set; }
        public string HinhAnh { get; set; }
    }
}
