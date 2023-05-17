using Guna.UI2.Licensing.LightJson.Serialization;
using Microsoft.Reporting.WinForms;
using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.LogicTier;
using QuanLyNhaHangE2.PresentationTier;
using QuanLyNhaHangE2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHangE2
{
    public partial class frmDoanhThu : Form
    {
        private HoaDonBUS hoaDonBUS;
        private NhanVienViewModel nhanVienBanHang;
        System.Globalization.CultureInfo fVND = new System.Globalization.CultureInfo("vi-VN");
        public frmDoanhThu(NhanVienViewModel nhanVienViewModel)
        {
            InitializeComponent();
            hoaDonBUS = new HoaDonBUS();
            nhanVienBanHang = nhanVienViewModel;
        }
        public List<HoaDon> TimDoanhThuTheoNgay(DateTime date1, DateTime date2)
        {
            QuanLyNhaHangModel quanLyNhaHangModel = new QuanLyNhaHangModel();
            var Result = quanLyNhaHangModel.HoaDons.Where(x => x.Ngay >= date1 && x.Ngay <= date2); 
            return Result.ToList();
        }
        private void LoadFormTheoNgay(DateTime date1, DateTime date2)
        {
            dgvDoanhThu.DataSource = TimDoanhThuTheoNgay(date1, date2);
        }
        private void frmDoanhThu_Load(object sender, EventArgs e)
        {
            rpvThongKe.Visible = false;
            btnThoat.Visible = false;
            btnInThongKe.Enabled = false;
            dtpTuNgay.Value = DateTime.Today;
            dtpDen.Value = DateTime.Today.AddDays(+1);
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dd-MM-yyyy";
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd-MM-yyyy";
            this.rpvThongKe.RefreshReport();
        }

        private void dtnThongKe_Click(object sender, EventArgs e)
        {
            btnInThongKe.Enabled = true;
            LoadFormTheoNgay(dtpTuNgay.Value, dtpDen.Value);
            dgvDoanhThu.Columns[7].Visible = false;
            dgvDoanhThu.Columns[8].Visible = false;
            dgvDoanhThu.Columns[9].Visible = false;
            List<HoaDon> hoaDons = TimDoanhThuTheoNgay(dtpTuNgay.Value, dtpDen.Value); 
            double tongDoanhThu = 0;
            foreach (var item in hoaDons)
            {
                tongDoanhThu += (double)item.TongTien; 
            }
            txtTong.Text = String.Format(fVND, "{0:C0}", tongDoanhThu);
            btnInThongKe.Enabled = true;
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            btnInThongKe.Enabled = false;
            rpvThongKe.Visible = false;
            btnThongKe.Visible = true;
            btnThoat.Visible = false;
            btnInThongKe.Visible = true;
            if (dtpTuNgay.Value >= dtpDen.Value)
            {
                btnThongKe.Enabled = false;
            }  
            else
            {
                btnThongKe.Enabled = true;
            }    
        }

        private void btnInThongKe_Click(object sender, EventArgs e)
        {
            rpvThongKe.Visible = true;
            btnInThongKe.Visible = false;
            btnThongKe.Visible = false;
            btnThoat.Visible = true;
            List<HoaDon> hoaDons = TimDoanhThuTheoNgay(dtpTuNgay.Value, dtpDen.Value);
            List<InThongKe> inHoaDons = new List<InThongKe>();
            foreach (HoaDon item in hoaDons)
            {
                InThongKe i = new InThongKe();
                i.MaHD = item.MaHD;
                i.Ngay = (DateTime)item.Ngay;
                i.TenNhanVien = item.NhanVien.Ten;
                i.TenBan = item.BanAn.TenBan;
                i.GiamGia = (double)item.GiamGia;
                i.TamTinh = (double)item.TamTinh;
                i.TongTien = (double)item.TongTien;
                inHoaDons.Add(i);

            }
            double tongTien = (double)hoaDons.Sum(x => x.TongTien);
            ReportParameter[] param = new ReportParameter[7];
            param[0] = new ReportParameter("Ngay", string.Format(DateTime.Now.ToString("dd")));
            param[1] = new ReportParameter("Thang", string.Format(DateTime.Now.ToString("MM")));
            param[2] = new ReportParameter("Nam", string.Format(DateTime.Now.ToString("yyyy")));
            param[3] = new ReportParameter("Ten", nhanVienBanHang.TenNhanVien);
            param[4] = new ReportParameter("BatDau", string.Format(dtpTuNgay.Value.ToString("dd/MM/yyyy")));
            param[5] = new ReportParameter("KetThuc", string.Format(dtpDen.Value.ToString("dd/MM/yyyy")));
            param[6] = new ReportParameter("TongCong", String.Format(fVND, "{0:C0}", tongTien));
            this.rpvThongKe.LocalReport.ReportPath = "./Reports/rptThongKeDoanhThu.rdlc";
            this.rpvThongKe.LocalReport.SetParameters(param);
            var reportDataSource = new ReportDataSource("ThongKeDataSet", inHoaDons);
            this.rpvThongKe.LocalReport.DataSources.Clear();
            this.rpvThongKe.LocalReport.DataSources.Add(reportDataSource);
            this.rpvThongKe.LocalReport.DisplayName = "Thống Kê Doanh Thu";
            this.rpvThongKe.RefreshReport();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            rpvThongKe.Visible = false;
            btnThoat.Visible = false;
            btnInThongKe.Visible = true;
            btnThongKe.Visible = true;
        }
    }
}
