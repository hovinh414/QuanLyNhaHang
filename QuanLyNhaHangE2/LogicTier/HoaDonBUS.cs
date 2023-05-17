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
    internal class HoaDonBUS
    {
        private HoaDonDAL hoaDonDAL;
        public HoaDonBUS()
        {
            hoaDonDAL = new HoaDonDAL();
        }
        public IEnumerable<HoaDonViewModel> LayDanhSachHoaDon()
        {
            return hoaDonDAL.LayDanhSachHoaDon();
        }
        public bool LuuHoaDon (HoaDon hoaDon)
        {
            try
            {
                return hoaDonDAL.LuuHoaDon(hoaDon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
