using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHangE2.DataTier
{
    internal class NhanVienDAL
    {
        private QuanLyNhaHangModel quanLyNhaHangModel;
        public NhanVienDAL()
        {
            quanLyNhaHangModel = new QuanLyNhaHangModel();
        }
        //Lấy tất cả nhan viên
        public IEnumerable<NhanVienViewModel> GetNhanViens()
        {
            var danhSachNhanVien = quanLyNhaHangModel.NhanViens.Select(x => new NhanVienViewModel
            {
                MaNhanVien = x.MaNhanVien,
                TenNhanVien = x.Ten,
                TenDangNhap = x.TenDangNhap,
                GioiTinh = x.GioiTinh.ToString(),
                ChucVu = x.ChucVu,
                DiaChi = x.DiaChi,
                HinhAnh = x.HinhAnh
            }).ToList();
            return danhSachNhanVien;
        }
        public IEnumerable<NhanVienViewModel> TimKiemNhanVien(string tenNhanVien)
        {
            var danhSachNhanVien = quanLyNhaHangModel.NhanViens.Select(x => new NhanVienViewModel
            {
                MaNhanVien = x.MaNhanVien,
                TenNhanVien = x.Ten,
                TenDangNhap = x.TenDangNhap,
                GioiTinh = x.GioiTinh.ToString(),
                ChucVu = x.ChucVu,
                DiaChi = x.DiaChi

            }).ToList();
            var result = danhSachNhanVien.Where(x => x.TenNhanVien.ToLower().Contains(tenNhanVien.ToLower()));
            return result.ToList();
        }
        //Lưu Nhân viên (thêm + sửa)
        public bool LuuNhanVien(NhanVien nhanVien)
        {
            try
            {//NhanVien nv = quanLyCafeModel.NhanViens.Where(x => x.MaNhanVien == nhanVien.MaNhanVien).FirstOrDefault();

                NhanVien nv = quanLyNhaHangModel.NhanViens.Where(x => x.MaNhanVien == nhanVien.MaNhanVien).FirstOrDefault();
                if (nv != null)
                {
                    nv.Ten = nhanVien.Ten;
                    nv.TenDangNhap = nhanVien.TenDangNhap;
                    nv.MatKhau = nhanVien.MatKhau;
                    nv.DiaChi = nhanVien.DiaChi;
                    nv.ChucVu = nhanVien.ChucVu;
                    nv.GioiTinh = nhanVien.GioiTinh;
                    nv.HinhAnh = nhanVien.HinhAnh;
                }
                else
                {
                    if (quanLyNhaHangModel.NhanViens.Any(x => x.TenDangNhap == nhanVien.TenDangNhap))
                        throw new Exception("Tên đăng nhập không được trùng");

                    quanLyNhaHangModel.NhanViens.Add(nhanVien);
                }
                quanLyNhaHangModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool LuuThemAnh(NhanVien nhanVien)
        {
            try
            {//NhanVien nv = quanLyCafeModel.NhanViens.Where(x => x.MaNhanVien == nhanVien.MaNhanVien).FirstOrDefault();

                NhanVien nv = quanLyNhaHangModel.NhanViens.Where(x => x.MaNhanVien == nhanVien.MaNhanVien).FirstOrDefault();
                if (nv != null)
                {
                    nv.Ten = nhanVien.Ten;
                    nv.TenDangNhap = nhanVien.TenDangNhap;
                    nv.DiaChi = nhanVien.DiaChi;
                    nv.ChucVu = nhanVien.ChucVu;
                    nv.GioiTinh = nhanVien.GioiTinh;
                    nv.HinhAnh = nhanVien.HinhAnh;
                }
                else
                {
                    if (quanLyNhaHangModel.NhanViens.Any(x => x.MaNhanVien == nhanVien.MaNhanVien))
                        throw new Exception("Mã nhân viên không được trùng");

                    quanLyNhaHangModel.NhanViens.Add(nhanVien);
                }
                quanLyNhaHangModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool XoaNhanVien(string tenNhanVien)
        {
            try
            {//NhanVien nv = quanLyCafeModel.NhanViens.Where(x => x.MaNhanVien == nhanVien.MaNhanVien).FirstOrDefault();

                NhanVien nv = quanLyNhaHangModel.NhanViens.Where(x => x.Ten == tenNhanVien).FirstOrDefault();
                if (nv != null)
                {
                    quanLyNhaHangModel.NhanViens.Remove(nv);
                }
                quanLyNhaHangModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NhanVien GetNhanVien(int maNhanVien)
        {
            return quanLyNhaHangModel.NhanViens.Where(x => x.MaNhanVien == maNhanVien).FirstOrDefault();
        }
        public bool KiemTraDangNhap(string tenDangNhap, string matKhau, out NhanVienViewModel nv)
        {
            var nhanVien = quanLyNhaHangModel.NhanViens
                .Where(x => x.TenDangNhap == tenDangNhap).FirstOrDefault();

            nv = new NhanVienViewModel();

            if (nhanVien == null)
                return false;

            if (BCrypt.Net.BCrypt.Verify(matKhau, nhanVien.MatKhau))
            {
                nv.TenNhanVien = nhanVien.Ten;
                nv.MaNhanVien = nhanVien.MaNhanVien;
                nv.TenDangNhap = nhanVien.TenDangNhap;
                nv.GioiTinh = nhanVien.GioiTinh.ToString();
                nv.ChucVu = nhanVien.ChucVu;
                nv.DiaChi = nhanVien.DiaChi;
                nv.HinhAnh = nhanVien.HinhAnh;
                return true;
            }
            return false;
        }
    }
}
