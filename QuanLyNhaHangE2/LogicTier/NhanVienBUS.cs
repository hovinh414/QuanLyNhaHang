using BCrypt.Net;
using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhaHangE2.ViewModel;

namespace QuanLyNhaHangE2.LogicTier
{
    internal class NhanVienBUS
    {
        private NhanVienDAL nhanVienDAL;
        public NhanVienBUS()
        {
            nhanVienDAL = new NhanVienDAL();
        }
        public IEnumerable<NhanVienViewModel> GetNhanViens()
        {
            return nhanVienDAL.GetNhanViens();
        }
        public IEnumerable<NhanVienViewModel> TimKiemNhanVien(string tenNhanVien)
        {
            return nhanVienDAL.TimKiemNhanVien(tenNhanVien);
        }
        public bool LuuNhanVien(NhanVien nhanVien)
        {
            try
            {
                nhanVien.MatKhau = BCrypt.Net.BCrypt.HashPassword(nhanVien.MatKhau);
                return nhanVienDAL.LuuNhanVien(nhanVien);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool LuuThemAnh(NhanVien nhanVien)
        {
            try
            {
                return nhanVienDAL.LuuThemAnh(nhanVien);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool XoaNhanVien(string tenNhanVien)
        {
            try
            {

                return nhanVienDAL.XoaNhanVien(tenNhanVien);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool KiemTraNhanVien(string tenDangNhap, string matKhau, out NhanVienViewModel nv)
        {
            return nhanVienDAL.KiemTraDangNhap(tenDangNhap, matKhau, out nv);
        }
    }
}
