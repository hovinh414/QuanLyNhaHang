using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHangE2.DataTier
{
    internal class MonDAL
    {
        private QuanLyNhaHangModel quanLyNhaHangModel;
        public MonDAL()
        {
            quanLyNhaHangModel = new QuanLyNhaHangModel();
        }
        public IEnumerable<Mon> GetMonTheoMaDanhMuc(int maDanhMuc)
        {
            return quanLyNhaHangModel.Mons.Where(x => x.MaDanhMuc == maDanhMuc).ToList();
        }
        public IEnumerable<MonAnViewModel> LayDanhSachMon()
        {
            
            var danhSachMonAn = quanLyNhaHangModel.Mons.Select(x => new MonAnViewModel
            {
                MaMonAn = x.MaMon,
                TenMonAn = x.Ten,
                DonGia = (double)x.GiaTien,
                MaDanhMuc = (int)x.MaDanhMuc
            }).ToList();
            return danhSachMonAn;
        }
        public IEnumerable<MonAnViewModel> TimKiemMon(string tenMon)
        {

            var danhSachMonAn = quanLyNhaHangModel.Mons.Select(x => new MonAnViewModel
            {
                MaMonAn = x.MaMon,
                TenMonAn = x.Ten,
                DonGia = (double)x.GiaTien,
                MaDanhMuc = (int)x.MaDanhMuc
            }).ToList();
            var result = danhSachMonAn.Where(x => x.TenMonAn.ToLower().Contains(tenMon.ToLower())); ;
            return result.ToList();
        }
        public bool LuuMonAn(Mon monAn)
        {
            try
            {

                Mon mon = quanLyNhaHangModel.Mons.Where(x => x.MaMon == monAn.MaMon).FirstOrDefault();
                if (mon != null)
                {
                    mon.Ten = monAn.Ten;
                    mon.GiaTien = monAn.GiaTien;
                    mon.MaDanhMuc = monAn.MaDanhMuc;
                }
                else
                {
                    if (quanLyNhaHangModel.Mons.Any(x => x.MaMon == monAn.MaMon))
                        throw new Exception("Mã món ăn không được trùng");
                    quanLyNhaHangModel.Mons.Add(monAn);
                }

                quanLyNhaHangModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool XoaMonAn(int maMonAn)
        {
            try
            {
                Mon mon = quanLyNhaHangModel.Mons.Where(x => x.MaMon == maMonAn).FirstOrDefault();
                if (mon != null)
                {
                    quanLyNhaHangModel.Mons.Remove(mon);
                    quanLyNhaHangModel.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Mon GetMonAn(int maMonAn)
        {
            return quanLyNhaHangModel.Mons.Where(x => x.MaMon == maMonAn).FirstOrDefault();
        }
    }
}
