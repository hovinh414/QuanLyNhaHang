using QuanLyNhaHangE2.ViewModel;
using QuanLyNhaHangE2.PresentationTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using QuanLyNhaHangE2.LogicTier;
using Microsoft.Reporting.WinForms;
using QuanLyNhaHangE2.DataTier.Model;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace QuanLyNhaHangE2
{

    public partial class frmGiaoDien : Form
    {
        private const int W = 80;// CHIEU RONG CUA BAN
        private const int H = 80; // CHIEU CAO CUA BAN
        const int COT = 5;
        const int _X = 100; // KHOANG CACH X
        const int _Y = 100; // KHOANG CACH Y
        private BanAnBUS banAnBUS;
        private DanhMucBUS danhMucBUS;
        private MonBUS monBUS;
        private HoaDonBUS hoaDonBUS;
        private double tongTien = 0;
        System.Globalization.CultureInfo fVND = new System.Globalization.CultureInfo("vi-VN");
        private Button btnBanChon = null;
        private List<ThongTinDatBan> DanhSachDatBan = new List<ThongTinDatBan>();
        private NhanVienViewModel nhanVienBanHang;
        public frmGiaoDien(NhanVienViewModel nv)
        {
            InitializeComponent();
            nhanVienBanHang = nv;
            lblNhanVien.Text = "Welcome: " + nv.TenNhanVien;
            nudGiamGia.TextChanged += nudGiamGia_TextChanged;
            if (nv.ChucVu == "admin")
            {
                btnNhanVien.Enabled = true;
                btnThucDon.Enabled = true;
                btnDoanhThu.Enabled = true;

            }
            else
            {
                btnNhanVien.Enabled = false;
                btnThucDon.Enabled = false;
                btnDoanhThu.Enabled = false;
            }
            CaiDatDieuKien();
            banAnBUS = new BanAnBUS();
            danhMucBUS = new DanhMucBUS();
            monBUS = new MonBUS();
            hoaDonBUS = new HoaDonBUS();
            LayTen();
            if (nv.HinhAnh == null)
            {
                return;
            }  
            else
            {
                picAvt.Image = Image.FromFile(nv.HinhAnh);
            }    
            
        }
        private void LayTen()
        {
            int TrangCuoi = 0;
            for (int i = nhanVienBanHang.TenNhanVien.Length; i > 0; i--)
            {
                if (nhanVienBanHang.TenNhanVien.Substring(i - 1, 1) == " ")
                {
                    TrangCuoi = i - 1;
                    break;
                }    
            }
            if (TrangCuoi > 0)
            {
                lblTen.Text = Convert.ToString(nhanVienBanHang.TenNhanVien.Substring(TrangCuoi + 1, nhanVienBanHang.TenNhanVien.Length - TrangCuoi - 1));
            }    
            else
            {
                lblTen.Text = "";
            }    
        }
        private void frmGiaoDien_Load(object sender, EventArgs e)
        {
            lblNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblGio.Text = DateTime.Now.ToString("HH:mm");
            lblThu.Text = DateTime.Now.ToString("dddd");
            plMon.Visible = false;
            plPhu.Visible = false;
            KhoiTaoSoBan();
            LoadDanhMuc();
            LoadBan();
            this.WindowState = FormWindowState.Maximized;
            this.rpvInBill.RefreshReport();
        }
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            btnThoat.Visible = false;
            plPhu.Visible = true;
            plPhu.Controls.Clear();
            frmNhanVien frm = new frmNhanVien(nhanVienBanHang);
            frm.TopLevel = false;
            plPhu.Controls.Add(frm);
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }
        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            btnThoat.Visible = false;
            plPhu.Visible = true;
            plPhu.Controls.Clear();
            frmTaiKhoan frm = new frmTaiKhoan(nhanVienBanHang);
            frm.TopLevel = false;
            plPhu.Controls.Add(frm);
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show(); 
        }
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            plChinh.Visible = true;
            plPhu.Visible = false;
            plMon.Visible = false;
            btnThoat.Visible = false;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            plOrder.Visible = true;
            plMon.Visible = true;
            plPhu.Visible = false;
            plChinh.Visible = false;
            btnThoat.Visible = false;
        }
        private void btnThucDon_Click(object sender, EventArgs e)
        {
            btnThoat.Visible = false;
            plPhu.Visible = true;
            plPhu.Controls.Clear();
            frmThucDon frm = new frmThucDon(nhanVienBanHang);
            frm.TopLevel = false;
            plPhu.Controls.Add(frm);
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }
        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            btnThoat.Visible = false;
            plPhu.Visible = true;
            plPhu.Controls.Clear();
            frmDoanhThu frm = new frmDoanhThu(nhanVienBanHang);
            frm.TopLevel = false;
            plPhu.Controls.Add(frm);
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
            this.Hide();
            nhanVienBanHang = null;
            //this.Close();
            frm.FormClosed += Frm_FormClosed;
        }

        private void picHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CaiDatDieuKien()
        {
            cmbBan.DisplayMember = "TenBan";
            cmbBan.ValueMember = "MaSoBan";
            cmbDanhMuc.DisplayMember = "Ten";
            cmbDanhMuc.ValueMember = "MaDanhMuc";
            cmbMon.DisplayMember = "Ten";
            cmbMon.ValueMember = "MaMon";
        }
        private void nudGiamGia_TextChanged(object sender, EventArgs e)
        {
            double tienGiam = tongTien * (double)(nudGiamGia.Value / 100);
            txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien - tienGiam);
        }
        private void TaoBan(int x, int y, BanAn ban)
        {
            Button btn = new Button();
            btn.Location = new Point(x, y);
            btn.Size = new Size(W, H);
            btn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btn.Text = ban.TenBan;
            btn.Tag = ban.MaSoBan;
            btn.Image = Image.FromFile("../../Resources/Ban.png");
            btn.BackColor = Color.White;
            btn.Click += Btn_Click;
            plOrder.Controls.Add(btn);
        }
        private void KhoiTaoSoBan()
        {
            int x = 30;
            int y = 48;
            int count = 0;
            foreach (BanAn ban in banAnBUS.GetBanAns())
            {
                TaoBan(x, y, ban);
                count++;
                if (count % COT == 0)
                {
                    y += _Y;
                    x = 30;
                    continue;
                }
                x += _X;
            }

        }
        private void Btn_Click(object sender, EventArgs e)
        {
            nudSoLuong.Value = 1;
            Button btn = (Button)sender;
            if (btnBanChon == null)
            {
                btn.BackColor = Color.LightBlue;
            }
            else if (btnBanChon != btn)
            {
                dgvMonAn.Rows.Clear();
                btnBanChon.BackColor = Color.White;
                btn.BackColor = Color.LightBlue;
                ThongTinDatBan thongTinDatBan = DanhSachDatBan.Where(x => x.MaBan == (int)btn.Tag).FirstOrDefault();
                if (thongTinDatBan != null)
                {
                    HienThiDanhSachMon(thongTinDatBan.DanhSachMon);
                    tongTien = thongTinDatBan.DanhSachMon.Sum(x => x.ThanhTien);
                    txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
                }
                else
                {
                    tongTien = 0;
                    txtTongTien.Clear();
                }
            }
            else
            {
                btn.BackColor = Color.White;
                btnBanChon = null;
                tongTien = 0;
                txtTongTien.Clear();
                return;
            }
            btnBanChon = btn;
        }
        private void HienThiDanhSachMon(List<MonDat> monDats)
        {
            dgvMonAn.Rows.Clear();
            foreach (MonDat mon in monDats)
            {
                int index = dgvMonAn.Rows.Add();
                dgvMonAn.Rows[index].Cells[0].Value = mon.TenMon;
                dgvMonAn.Rows[index].Cells[1].Value = mon.SoLuong;
                dgvMonAn.Rows[index].Cells[2].Value = mon.DonGia;
                dgvMonAn.Rows[index].Cells[3].Value = mon.ThanhTien;
            }
        }
        private void LoadDanhMuc()
        {
            cmbDanhMuc.DataSource = danhMucBUS.LayDanhMuc();
        }
        private void LoadBan()
        {
            cmbBan.DataSource = banAnBUS.GetBanAns();
        }
        private void cmbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            int maDanhMuc = int.Parse(cmb.SelectedValue.ToString());
            cmbMon.DataSource = monBUS.GetMonsTheoDanhMuc(maDanhMuc);
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (btnBanChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi đặt món!", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            if (nudSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            int maBan = int.Parse(btnBanChon.Tag.ToString());
            ThongTinDatBan thongTinDatBan = DanhSachDatBan.Where(x => x.MaBan.ToString() == btnBanChon.Tag.ToString()).FirstOrDefault();

            //Trường hợp bàn chưa đặt
            if (thongTinDatBan == null)
            {
                thongTinDatBan = new ThongTinDatBan();
                thongTinDatBan.MaBan = maBan;
                DanhSachDatBan.Add(thongTinDatBan);
                btnBanChon.Image = Image.FromFile("../../Resources/DangSuDung.png");
            }
            //Thêm món vào bàn
            Mon monChon = (Mon)cmbMon.SelectedItem;
            MonDat monMoi = new MonDat();
            {
                monMoi.MaMon = monChon.MaMon;
                monMoi.TenMon = monChon.Ten;
                monMoi.DonGia = (double)monChon.GiaTien;
                monMoi.SoLuong = (int)nudSoLuong.Value;
            }
            thongTinDatBan.CapNhatMon(monMoi);

            tongTien = thongTinDatBan.DanhSachMon.Sum(x => x.ThanhTien);
            txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
            HienThiDanhSachMon(thongTinDatBan.DanhSachMon);
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            int flag = -1;
            if (btnBanChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi xóa món!", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            ThongTinDatBan thongTinDatBan = DanhSachDatBan.Where(x => x.MaBan.ToString() == btnBanChon.Tag.ToString()).FirstOrDefault();
            if (thongTinDatBan == null)
            {
                MessageBox.Show("Bàn trống không thể xóa!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            foreach (MonDat mon in thongTinDatBan.DanhSachMon.ToList())
            {
                if (txtMon.Text == mon.TenMon)
                {
                    thongTinDatBan.DanhSachMon.Remove(mon);
                    flag = 1;
                }
            }
            if (thongTinDatBan.DanhSachMon.Count() == 0)
            {
                btnBanChon.Image = Image.FromFile("../../Resources/Ban.png");
                btnBanChon.BackColor = Color.White;
            }
            else if (flag == -1)
            {
                MessageBox.Show("Không có món cần xóa!", "Thông Báo", MessageBoxButtons.OK);
            }
            tongTien = thongTinDatBan.DanhSachMon.Sum(x => x.ThanhTien);
            txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
            HienThiDanhSachMon(thongTinDatBan.DanhSachMon);
            txtMon.Text = "";
        }

        private void btnDoiBan_Click(object sender, EventArgs e)
        {

            if (btnBanChon == null)
            {
                MessageBox.Show("Vui lòng chọn 1 bàn trước khi chuyển!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            int maBanMuonDen = int.Parse(cmbBan.SelectedValue.ToString());
            int maBanHienTai = int.Parse(btnBanChon.Tag.ToString());
            if (maBanMuonDen == maBanHienTai)
            {
                MessageBox.Show("Không được chuyển 2 bàn giống nhau!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            ThongTinDatBan thongTinBanDen = DanhSachDatBan.Where(x => x.MaBan == maBanMuonDen).FirstOrDefault();
            ThongTinDatBan thongTinBanHienTai = DanhSachDatBan.Where(x => x.MaBan == maBanHienTai).FirstOrDefault();
            if (thongTinBanHienTai == null)
            {
                MessageBox.Show("Bàn hiện tại trống! Không thể chuyển", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            dgvMonAn.Rows.Clear();
            //Nếu bàn muốn đến còn trống
            if (thongTinBanDen == null)
            {
                thongTinBanHienTai.MaBan = maBanMuonDen;
                HienThiDanhSachMon(thongTinBanHienTai.DanhSachMon);
            }
            else
            {
                foreach (MonDat mon in thongTinBanHienTai.DanhSachMon)
                {
                    thongTinBanDen.CapNhatMon(mon);
                }
                HienThiDanhSachMon(thongTinBanDen.DanhSachMon);
            }
            btnBanChon.Image = Image.FromFile("../../Resources/Ban.png");
            btnBanChon.BackColor = Color.White;
            Button banChon = plOrder.Controls.OfType<Button>().Where(x => x.Tag.ToString() == maBanMuonDen.ToString()).FirstOrDefault();
            banChon.Image = Image.FromFile("../../Resources/DangSuDung.png");
            banChon.BackColor = Color.LightBlue;
            
        }
        private void dgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtMon.Text = dgvMonAn.Rows[rowIndex].Cells[0].Value.ToString();
            nudSoLuong.Value = decimal.Parse(dgvMonAn.Rows[rowIndex].Cells[1].Value.ToString());
        }
        private void btnInBill_Click(object sender, EventArgs e)
        {
            
            if (btnBanChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi in hóa đơn!", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            ThongTinDatBan thongTinDatBan = DanhSachDatBan.Where(x => x.MaBan.ToString() == btnBanChon.Tag.ToString()).FirstOrDefault();
            if (thongTinDatBan == null)
            {
                MessageBox.Show("Bàn Trống không có món để in!", "Thông Báo", MessageBoxButtons.OK);
                return;
            }

            plPhu.Controls.Clear();
            btnThoat.Visible = true;
            plPhu.Visible = true;
            plPhu.Controls.Add(rpvInBill);
            double tamTinh = 0;
            double tienGiam = tongTien * (double)(nudGiamGia.Value / 100);
            foreach (MonDat mon in thongTinDatBan.DanhSachMon)
            {
                tamTinh += mon.ThanhTien;
            }
            ReportParameter[] param = new ReportParameter[6];
            param[0] = new ReportParameter("Ngay", string.Format(DateTime.Now.ToString("dd/MM/yyyy")));
            param[1] = new ReportParameter("Ban", btnBanChon.Text);
            param[2] = new ReportParameter("TenNV", nhanVienBanHang.TenNhanVien);
            param[3] = new ReportParameter("TamTinh", String.Format(fVND, "{0:C0}", tamTinh));
            param[4] = new ReportParameter("GiamGia", nudGiamGia.Value.ToString() + "% (" + String.Format(fVND, "{0:C0}", tienGiam) + ")");
            param[5] = new ReportParameter("TongCong", txtTongTien.Text);
            this.rpvInBill.LocalReport.ReportPath = "./Reports/rptInHoaDon.rdlc";
            this.rpvInBill.LocalReport.SetParameters(param);
            var reportDataSource = new ReportDataSource("MonDataSet", thongTinDatBan.DanhSachMon);
            this.rpvInBill.LocalReport.DataSources.Clear();
            this.rpvInBill.LocalReport.DataSources.Add(reportDataSource);
            this.rpvInBill.LocalReport.DisplayName = "Hóa Đơn Bán Hàng";
            this.rpvInBill.RefreshReport();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            //plBill.Visible = false;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (btnBanChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn", "Thông báo", MessageBoxButtons.OKCancel);
                return;
            }
            int maSoBanChon = Convert.ToInt32(btnBanChon.Tag);
            ThongTinDatBan thongTin = DanhSachDatBan.Where(x => x.MaBan == maSoBanChon).FirstOrDefault();

            if (thongTin == null)
            {
                MessageBox.Show("Bàn không có món không thể thanh toán", "Thông báo", MessageBoxButtons.OKCancel);
                return;
            }
            double tienGiam = tongTien * (double)(nudGiamGia.Value / 100);
            DialogResult result = MessageBox.Show("Bạn có muốn thanh toán không!", "Thông báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;

            HoaDon hoaDon = new HoaDon();
            hoaDon.MaSoBan = maSoBanChon;
            hoaDon.Ngay = DateTime.Now;
            hoaDon.TamTinh = tongTien;
            hoaDon.GiamGia = (double)nudGiamGia.Value;
            hoaDon.TongTien = tongTien - tienGiam;
            hoaDon.MaNhanVien = nhanVienBanHang.MaNhanVien;


            //Lưu chi tiết 
            ChiTietHoaDon chiTiet;
            foreach (var item in thongTin.DanhSachMon)
            {
                chiTiet = new ChiTietHoaDon();
                chiTiet.MaMon = item.MaMon;
                chiTiet.SoLuong = item.SoLuong;
                hoaDon.ChiTietHoaDons.Add(chiTiet);
            }
            try
            {
                hoaDonBUS.LuuHoaDon(hoaDon);
                btnBanChon.BackColor = Color.LightBlue;
                btnBanChon.Image = Image.FromFile("../../Resources/Ban.png");
                dgvMonAn.Rows.Clear();
                nudGiamGia.ResetText();
                tongTien = 0;
                txtTongTien.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            int maBan = int.Parse(btnBanChon.Tag.ToString());
            ThongTinDatBan thongTinDatBan = DanhSachDatBan.Where(x => x.MaBan.ToString() == btnBanChon.Tag.ToString()).FirstOrDefault();
            thongTinDatBan.DanhSachMon.Clear();
            txtTongTien.Clear();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            plPhu.Visible = false;
            btnThoat.Visible = false;
        }
    }
}
