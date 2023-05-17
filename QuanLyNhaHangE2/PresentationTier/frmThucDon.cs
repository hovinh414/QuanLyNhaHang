using Guna.UI2.Licensing.LightJson.Serialization;
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
    public partial class frmThucDon : Form
    {
        private readonly MonBUS monBUS;
        private readonly DanhMucBUS danhMucBUS;
        private int maMonAn = -1;
        public frmThucDon(NhanVienViewModel nhanVienViewModel)
        {
            InitializeComponent();
            monBUS = new MonBUS();
            danhMucBUS = new DanhMucBUS();
            CaiDatDieuKien();
            txtMaMonAn.Enabled = false;
        }

        private void LoadDanhSachMonAn()
        {
            dgvMonAn.DataSource = monBUS.LayDanhSachMon();
        }

        private void CaiDatDieuKien()
        {
            cmbDanhMuc.DisplayMember = "Ten";
            cmbDanhMuc.ValueMember = "MaDanhMuc";
        }
        public void LoadMonTimKiem(string tenMon)
        {

            dgvMonAn.DataSource = monBUS.TimKiemMon(tenMon);

        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            maMonAn = -1;
            txtMaMonAn.Text = txtTenMonAn.Text = txtMaDanhMuc.Text = txtDonGia.Text = txtTimKiem.Text = cmbDanhMuc.Text = "";
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
        }
        private void dgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtMaMonAn.Text = dgvMonAn.Rows[rowIndex].Cells[0].Value.ToString();
            txtTenMonAn.Text = dgvMonAn.Rows[rowIndex].Cells[1].Value.ToString();
            txtDonGia.Text = dgvMonAn.Rows[rowIndex].Cells[3].Value.ToString();
            txtMaDanhMuc.Text = dgvMonAn.Rows[rowIndex].Cells[2].Value.ToString();
            cmbDanhMuc.SelectedValue = int.Parse(dgvMonAn.Rows[rowIndex].Cells[2].Value.ToString());
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text) == true)
            {
                LoadDanhSachMonAn();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Mon mon = new Mon();
            mon.Ten = txtTenMonAn.Text;
            mon.GiaTien = double.Parse(txtDonGia.Text);
            mon.MaDanhMuc = Convert.ToInt32(cmbDanhMuc.SelectedValue);
            if (maMonAn != -1)
            {
                mon.MaMon = maMonAn;
            }
            try
            {
                monBUS.LuuMonAn(mon);
                txtMaMonAn.Text = txtTenMonAn.Text = txtDonGia.Text = cmbDanhMuc.Text = "";
                LoadDanhSachMonAn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Mon mon = new Mon();
            mon.MaMon = int.Parse(txtMaMonAn.Text);
            mon.Ten = txtTenMonAn.Text;
            mon.GiaTien = double.Parse(txtDonGia.Text);
            mon.MaDanhMuc = Convert.ToInt32(cmbDanhMuc.SelectedValue);
            if (maMonAn != -1)
            {
                mon.MaMon = maMonAn;
            }
            try
            {
                monBUS.LuuMonAn(mon);
                txtMaMonAn.Text = txtTenMonAn.Text = txtDonGia.Text = cmbDanhMuc.Text = "";
                LoadDanhSachMonAn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {

                int mon = int.Parse(txtMaMonAn.Text);
                monBUS.XoaMonAn(mon);
                txtMaMonAn.Text = lblTenMonAn.Text = txtDonGia.Text = cmbDanhMuc.Text = "";
                LoadDanhSachMonAn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadMonTimKiem(txtTimKiem.Text);
        }

        private void frmQuanLyMonAn_Load(object sender, EventArgs e)
        {
            cmbDanhMuc.DataSource = danhMucBUS.LayDanhMuc();
            LoadDanhSachMonAn();
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = txtMaDanhMuc.Enabled = false;
            dgvMonAn.Columns[0].HeaderText = "Mã Món Ăn";
            dgvMonAn.Columns[1].HeaderText = "Tên Món Ăn";
            dgvMonAn.Columns[2].HeaderText = "Giá Tiền";
            dgvMonAn.Columns[3].HeaderText = "Mã Danh Mục";
        }
        private void NhapThongTin(object sender, EventArgs e)
        {
            if (maMonAn == -1)
            {
                btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled =
                    !string.IsNullOrWhiteSpace(txtTenMonAn.Text) &&
                    !string.IsNullOrWhiteSpace(txtDonGia.Text) &&
                    !string.IsNullOrWhiteSpace(cmbDanhMuc.Text);
            }
            else
            {
                btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = !string.IsNullOrWhiteSpace(txtTenMonAn.Text);
            }
        }
    }
}
