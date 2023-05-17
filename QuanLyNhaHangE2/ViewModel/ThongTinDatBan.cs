using QuanLyNhaHangE2.DataTier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHangE2.ViewModel
{
    internal class ThongTinDatBan
    {
        public int MaBan { get; set; }
        public List<MonDat> DanhSachMon { get; set; }
        public ThongTinDatBan()
        {
            DanhSachMon = new List<MonDat>();
        }
        //Thêm Món và cập nhật món
        public void CapNhatMon(MonDat mon)
        {
            MonDat m = DanhSachMon.Where(x => x.MaMon == mon.MaMon).FirstOrDefault();
            if (m != null)
            {
                m.SoLuong += mon.SoLuong;
            }
            else
            {
                DanhSachMon.Add(mon);
            }
        }
    }
}
