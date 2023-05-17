using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.LogicTier;
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
using TheArtOfDevHtmlRenderer.Adapters;

namespace QuanLyNhaHangE2.PresentationTier
{
    public partial class frmNhanVien : Form
    {
        private readonly NhanVienBUS nhanVienBUS;
        private int maNhanVien = -1;
        public frmNhanVien(NhanVienViewModel nhanVienBanHang)
        {
            InitializeComponent();
            btnLuu.Enabled = false;
            this.Load += frmNhanVien_Load;
            nhanVienBUS = new NhanVienBUS();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanVien();
            btnTim.Enabled = btnLuu.Enabled = btnXoa.Enabled = false;
            txtLink.Text = "D:\\Anhhhh\\Account.jpg";
        }
        private void LoadDanhSachNhanVien()
        {
            dgvNhanVien.DataSource = nhanVienBUS.GetNhanViens();
            for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
            {
                if ((string)dgvNhanVien.Rows[i].Cells[3].Value == "True")
                {
                    dgvNhanVien.Rows[i].Cells[3].Value = "Nữ";
                }
                else
                {
                    dgvNhanVien.Rows[i].Cells[3].Value = "Nam";
                }
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.Ten = txtDisplayName.Text;
            nv.TenDangNhap = txtUserName.Text;
            nv.MatKhau = txtMatKhau.Text;
            nv.ChucVu = txtPosition.Text;
            nv.DiaChi = txtAddress.Text;
            nv.HinhAnh = txtLink.Text;
            if (rdFemale.Checked)
            {
                nv.GioiTinh = true;
            }
            else
            {
                nv.GioiTinh = false;
            }
            if (maNhanVien != -1)
                nv.MaNhanVien = maNhanVien;
            try
            {
                nhanVienBUS.LuuNhanVien(nv);
                LoadDanhSachNhanVien();
                txtDisplayName.Text = txtUserName.Text = txtMatKhau.Text = txtAddress.Text = txtPosition.Text = "";
                rdFemale.Checked = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            maNhanVien = -1;
            txtLink.Text = "D:\\Anhhhh\\Account.jpg";
        }

        private void txtThongTin_TextChanged(object sender, EventArgs e)
        {

            btnLuu.Enabled =
                !string.IsNullOrWhiteSpace(txtUserName.Text) &&
                !string.IsNullOrWhiteSpace(txtDisplayName.Text) &&
                !string.IsNullOrWhiteSpace(txtPosition.Text) &&
                !string.IsNullOrWhiteSpace(txtMatKhau.Text) &&
                !string.IsNullOrWhiteSpace(txtAddress.Text);
            btnXoa.Enabled =
                !string.IsNullOrWhiteSpace(txtDisplayName.Text);
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            maNhanVien = Convert.ToInt32(dgvNhanVien.Rows[rowIndex].Cells[0].Value);
            txtDisplayName.Text = dgvNhanVien.Rows[rowIndex].Cells[1].Value.ToString();
            txtUserName.Text = dgvNhanVien.Rows[rowIndex].Cells[2].Value.ToString();
            txtPosition.Text = dgvNhanVien.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvNhanVien.Rows[rowIndex].Cells[5].Value.ToString();

            if ((string)dgvNhanVien.Rows[rowIndex].Cells[3].Value == "Nữ")
            {
                rdFemale.Checked = true;
            }
            else
            {
                rdMale.Checked = true;
            }

            if (dgvNhanVien.Rows[rowIndex].Cells[6].Value == null)
            {
                return;
            }
            else
            {
                txtLink.Text = dgvNhanVien.Rows[rowIndex].Cells[6].Value.ToString();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            maNhanVien = -1;
            txtDisplayName.Text = txtAddress.Text = txtUserName.Text = txtPosition.Text = txtTimKiem.Text = "";
            txtLink.Text = "D:\\Anhhhh\\Account.jpg";
            rdFemale.Checked = rdMale.Checked = false;
            btnLuu.Enabled = btnXoa.Enabled = false;
        }

        private void dgvNhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                btnXoa.PerformClick();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.Ten = txtDisplayName.Text;
            nv.TenDangNhap = txtUserName.Text;
            nv.MatKhau = txtMatKhau.Text;
            nv.DiaChi = txtAddress.Text;
            nv.ChucVu = txtPosition.Text;
            if (rdFemale.Checked)
                nv.GioiTinh = true;
            nv.GioiTinh = false;
            if (maNhanVien != -1)
                nv.MaNhanVien = maNhanVien;
            try
            {
                nhanVienBUS.XoaNhanVien(nv.Ten);
                txtDisplayName.Text = txtAddress.Text = txtMatKhau.Text = txtUserName.Text = "";
                txtLink.Text = "D:\\Anhhhh\\Account.jpg";
                rdFemale.Checked = true;
                LoadDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (cbTimTen.Checked && cbTimDC.Checked == false && cbTimVT.Checked == false)
            {
                dgvNVView.Rows.Clear();
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    if (dgvNhanVien[1, i].Value.ToString().ToUpper().Contains(txtTimTen.Text.ToUpper()))
                    {
                        dgvNVView.Rows.Add(dgvNhanVien[0, i].Value, dgvNhanVien[1, i].Value, dgvNhanVien[2, i].Value, dgvNhanVien[4, i].Value, dgvNhanVien[5, i].Value);
                    }
                }
            }
            else if (cbTimTen.Checked == false && cbTimDC.Checked == false && cbTimVT.Checked)
            {
                dgvNVView.Rows.Clear();
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    if (dgvNhanVien[4, i].Value.ToString().ToUpper().Contains(txtTimVT.Text.ToUpper()))
                    {
                        dgvNVView.Rows.Add(dgvNhanVien[0, i].Value, dgvNhanVien[1, i].Value, dgvNhanVien[2, i].Value, dgvNhanVien[4, i].Value, dgvNhanVien[5, i].Value);
                    }
                }
            }
            else if (cbTimTen.Checked == false && cbTimDC.Checked && cbTimVT.Checked == false)
            {
                dgvNVView.Rows.Clear();
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    if (dgvNhanVien[5, i].Value.ToString().ToUpper().Contains(txtTimDC.Text.ToUpper()))
                    {
                        dgvNVView.Rows.Add(dgvNhanVien[0, i].Value, dgvNhanVien[1, i].Value, dgvNhanVien[2, i].Value, dgvNhanVien[4, i].Value, dgvNhanVien[5, i].Value);
                    }
                }
            }
            else if (cbTimTen.Checked && cbTimDC.Checked == false && cbTimVT.Checked)
            {
                dgvNVView.Rows.Clear();
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    if (dgvNhanVien[4, i].Value.ToString().ToUpper().Contains(txtTimVT.Text.ToUpper()) && dgvNhanVien[1, i].Value.ToString().ToUpper().Contains(txtTimTen.Text.ToUpper()))
                    {
                        dgvNVView.Rows.Add(dgvNhanVien[0, i].Value, dgvNhanVien[1, i].Value, dgvNhanVien[2, i].Value, dgvNhanVien[4, i].Value, dgvNhanVien[5, i].Value);
                    }
                }
            }
            else if (cbTimTen.Checked == false && cbTimDC.Checked && cbTimVT.Checked)
            {
                dgvNVView.Rows.Clear();
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    if (dgvNhanVien[4, i].Value.ToString().ToUpper().Contains(txtTimVT.Text.ToUpper()) && dgvNhanVien[5, i].Value.ToString().ToUpper().Contains(txtTimDC.Text.ToUpper()))
                    {
                        dgvNVView.Rows.Add(dgvNhanVien[0, i].Value, dgvNhanVien[1, i].Value, dgvNhanVien[2, i].Value, dgvNhanVien[4, i].Value, dgvNhanVien[5, i].Value);
                    }
                }
            }
            else if (cbTimTen.Checked && cbTimDC.Checked && cbTimVT.Checked == false)
            {
                dgvNVView.Rows.Clear();
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    if (dgvNhanVien[1, i].Value.ToString().ToUpper().Contains(txtTimTen.Text.ToUpper()) && dgvNhanVien[5, i].Value.ToString().ToUpper().Contains(txtTimDC.Text.ToUpper()))
                    {
                        dgvNVView.Rows.Add(dgvNhanVien[0, i].Value, dgvNhanVien[1, i].Value, dgvNhanVien[2, i].Value, dgvNhanVien[4, i].Value, dgvNhanVien[5, i].Value);
                    }
                }
            }
            else if (cbTimTen.Checked && cbTimDC.Checked && cbTimVT.Checked)
            {
                dgvNVView.Rows.Clear();
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    if (dgvNhanVien[4, i].Value.ToString().ToUpper().Contains(txtTimVT.Text.ToUpper()) && dgvNhanVien[5, i].Value.ToString().ToUpper().Contains(txtTimDC.Text.ToUpper()) && dgvNhanVien[1, i].Value.ToString().ToUpper().Contains(txtTimDC.Text.ToUpper()))
                    {
                        dgvNVView.Rows.Add(dgvNhanVien[0, i].Value, dgvNhanVien[1, i].Value, dgvNhanVien[2, i].Value, dgvNhanVien[4, i].Value, dgvNhanVien[5, i].Value);
                    }
                }
            }
        }

        private void cbTimTen_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTimDC.Checked == true || cbTimTen.Checked == true || cbTimVT.Checked == true)
            {
                btnTim.Enabled = true;
            }
            else
            {
                btnTim.Enabled = false;
            }
        }
        public void LoadNhanVienTimKiem(string tenNhanVien)
        {

            dgvNhanVien.DataSource = nhanVienBUS.TimKiemNhanVien(tenNhanVien);
            for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
            {
                if ((string)dgvNhanVien.Rows[i].Cells[3].Value == "True")
                {
                    dgvNhanVien.Rows[i].Cells[3].Value = "Nữ";
                }
                else
                {
                    dgvNhanVien.Rows[i].Cells[3].Value = "Nam";
                }
            }

        }
        private void btnTimNhanh_Click(object sender, EventArgs e)
        {
            LoadNhanVienTimKiem(txtTimKiem.Text);


        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text) == true)
            {
                LoadDanhSachNhanVien();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp; ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtLink.Text = dlg.FileName;
            }
        }
    }
}
