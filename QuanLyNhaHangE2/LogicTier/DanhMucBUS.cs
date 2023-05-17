using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHangE2.LogicTier
{
    internal class DanhMucBUS
    {
        private DanhMucDAL danhMucDAL;
        public DanhMucBUS()
        {
            danhMucDAL = new DanhMucDAL();
        }
        public IEnumerable<DanhMuc> GetDanhMucs(string tenDanhMuc)
        {
            return danhMucDAL.GetDanhMucs(tenDanhMuc);
        }
        public IEnumerable<DanhMuc> LayDanhMuc()
        {
            return danhMucDAL.LayDanhMuc();
        }
    }
}
