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
    internal class MonBUS
    {
        private MonDAL monDAL;
        public MonBUS()
        {
            monDAL = new MonDAL();
        }
        public IEnumerable<Mon> GetMonsTheoDanhMuc(int maDanhMuc)
        {
            return monDAL.GetMonTheoMaDanhMuc(maDanhMuc);
        }
        public IEnumerable<MonAnViewModel> LayDanhSachMon()
        {
            return monDAL.LayDanhSachMon();
        }
        public IEnumerable<MonAnViewModel> TimKiemMon(string tenMon)
        {
            return monDAL.TimKiemMon(tenMon);
        }
        public bool LuuMonAn(Mon mon)
        {
            try
            {
                return monDAL.LuuMonAn(mon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool XoaMonAn(int food)
        {
            try
            {
                return monDAL.XoaMonAn(food);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
