using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHangE2.LogicTier
{
    internal class BanAnBUS
    {
        private BanAnDAL banAnDAL;
        public BanAnBUS()
        {
            banAnDAL = new BanAnDAL();
        }
        public IEnumerable<BanAn> GetBanAns()
        {
            return banAnDAL.GetBanAns();
        }
    }
}
