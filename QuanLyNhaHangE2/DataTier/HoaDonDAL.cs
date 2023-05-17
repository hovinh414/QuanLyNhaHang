using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhaHangE2.ViewModel;

namespace QuanLyNhaHangE2.DataTier
{
    internal class HoaDonDAL
    {
        private QuanLyNhaHangModel quanLyNhaHangModel;
        public HoaDonDAL()
        {
            quanLyNhaHangModel = new QuanLyNhaHangModel();
        }
        public IEnumerable<HoaDonViewModel> LayDanhSachHoaDon()
        {
            var danhSachMonAn = quanLyNhaHangModel.HoaDons.Select(x => new HoaDonViewModel
            {
                MaSoBan = (int)x.MaSoBan,
                MaHD = x.MaHD,
                TongTien = (double)x.TongTien,
                GiamGia = (double)x.GiamGia,
                TamTinh = (double)x.TamTinh,
                Ngay = (DateTime)x.Ngay,
                MaNhanVien = (int)x.MaNhanVien,
            }).ToList();
            return danhSachMonAn;
        }
        public bool LuuHoaDon(HoaDon hoaDon)
        {
            try
            {
                quanLyNhaHangModel.HoaDons.Add(hoaDon);
                quanLyNhaHangModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
