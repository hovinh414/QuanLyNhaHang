using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHangE2.DataTier
{
    internal class DanhMucDAL
    {
        private QuanLyNhaHangModel quanLyNhaHangModel;
        public DanhMucDAL()
        {
            quanLyNhaHangModel = new QuanLyNhaHangModel();
        }
        public IEnumerable<DanhMuc> GetDanhMucs(string tenDanhMuc)
        {
            return quanLyNhaHangModel.DanhMucs.Where(x => x.Ten == tenDanhMuc).ToList();
        }
        public IEnumerable<DanhMuc> LayDanhMuc()
        {
            return quanLyNhaHangModel.DanhMucs.ToList();
        }
    }
}
