using QuanLyNhaHangE2.DataTier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHangE2.DataTier
{
    internal class BanAnDAL
    {
        private QuanLyNhaHangModel quanLyNhaHangModel;
        public BanAnDAL()
        {
            quanLyNhaHangModel = new QuanLyNhaHangModel();
        }
        public IEnumerable<BanAn> GetBanAns()
        {
            return quanLyNhaHangModel.BanAns.ToList();
        }
    }
}
