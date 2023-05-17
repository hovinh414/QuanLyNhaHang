using Guna.UI2.Licensing.LightJson.Serialization;
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

namespace QuanLyNhaHangE2.PresentationTier
{
    public partial class frmDangNhap : Form
    {
        private NhanVienBUS nhanVienBUS;
        public frmDangNhap()
        {
            InitializeComponent();
            nhanVienBUS = new NhanVienBUS();
        }
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            rbtnLuuMatKhau.Checked = true;
            btnDangNhap.Enabled = false;
            btnAnMK.Image = Image.FromFile("../../Resources/AnMK.png");
            if (Properties.Settings.Default.TenDangNhap != String.Empty)
            {
                txtTenDangNhap.Text = Properties.Settings.Default.TenDangNhap;
                txtMatKhau.Text = Properties.Settings.Default.MatKhau;
                rbtnLuuMatKhau.Checked = true;
            }
        }
        private void btnAnHien_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '•')
            {
                txtMatKhau.PasswordChar = '\0';
                btnAnMK.Image = Image.FromFile("../../Resources/HienMK.png");
            }    
                
            else
            {
                txtMatKhau.PasswordChar = '•';
                btnAnMK.Image = Image.FromFile("../../Resources/AnMK.png");
            }    
                
        }
        private void txtThongTin_TextChanged(object sender, EventArgs e)
        {

            btnDangNhap.Enabled =
                !string.IsNullOrWhiteSpace(txtTenDangNhap.Text) &&
                !string.IsNullOrWhiteSpace(txtMatKhau.Text);

        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (rbtnLuuMatKhau.Checked)
            {
                Properties.Settings.Default.TenDangNhap = txtTenDangNhap.Text;
                Properties.Settings.Default.MatKhau = txtMatKhau.Text;
            }
            else
            {
                Properties.Settings.Default.TenDangNhap = "";
                Properties.Settings.Default.MatKhau = "";
            }
            Properties.Settings.Default.Save();
            NhanVienViewModel nv;
            if (nhanVienBUS.KiemTraNhanVien(txtTenDangNhap.Text, txtMatKhau.Text, out nv))
            {
                frmGiaoDien frm = new frmGiaoDien(nv);
                frm.Show();
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Hide();
                frm.FormClosed += btnThoat_Click;
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công", "Thông Báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
