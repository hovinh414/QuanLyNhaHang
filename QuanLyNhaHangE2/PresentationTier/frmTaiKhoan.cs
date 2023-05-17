using Guna.UI2.Licensing.LightJson.Serialization;
using QuanLyNhaHangE2.DataTier.Model;
using QuanLyNhaHangE2.LogicTier;
using QuanLyNhaHangE2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TheArtOfDevHtmlRenderer.Adapters;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace QuanLyNhaHangE2.PresentationTier
{
    public partial class frmTaiKhoan : Form
    {
        public NhanVienViewModel nv;
        public frmTaiKhoan(NhanVienViewModel nhanVienBanHang)
        {
            InitializeComponent();
            nhanVienBUS = new NhanVienBUS();
            nv = nhanVienBanHang;
            txtMaNhanVien.Text = nhanVienBanHang.MaNhanVien.ToString();
            
        }
        private readonly NhanVienBUS nhanVienBUS;
        private int maNhanVien = -1;
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.MaNhanVien = int.Parse(txtMaNhanVien.Text);
            nv.Ten = txtTenNhanVien.Text;
            nv.TenDangNhap = txtDangNhap.Text;
            nv.ChucVu = txtChucVu.Text;
            nv.DiaChi = txtDiaChi.Text;
            string pathstring = txtLinkAnh.Text;
            nv.HinhAnh = pathstring;
            if (rdNu.Checked)
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
                nhanVienBUS.LuuThemAnh(nv);
                //LoadDanhSachNhanVien();
                picAnh.Image = Image.FromFile(pathstring);
            }
   
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Cập nhật thông tin thành công!\nThông tin sẽ được cập nhật sau khi đăng nhập lại chương trình!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void txtThongTin_TextChanged(object sender, EventArgs e)
        {

            btnCapNhat.Enabled =
                   !string.IsNullOrWhiteSpace(txtTenNhanVien.Text) &&
                   !string.IsNullOrWhiteSpace(txtDiaChi.Text) &&
                   !string.IsNullOrWhiteSpace(txtDangNhap.Text);

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox p = sender as PictureBox;
            open.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp; ";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtLinkAnh.Text = open.FileName;
                p.Image = Image.FromFile(txtLinkAnh.Text);

                btnCapNhat.Enabled = true;
            }
            if (p == null)
            {
                MessageBox.Show("ok");
            }

        }

        private void Account_Load(object sender, EventArgs e)
        {
            txtMaNhanVien.Enabled = false;
            
            foreach (NhanVienViewModel i in nhanVienBUS.GetNhanViens().ToList())
            {
                if (i.MaNhanVien == int.Parse(txtMaNhanVien.Text))
                {
                    NhanVienViewModel temp = new NhanVienViewModel();
                    temp = i;
                    txtDangNhap.Text = temp.TenDangNhap;
                    txtChucVu.Text = temp.ChucVu;
                    txtDiaChi.Text = temp.DiaChi;
                    txtTenNhanVien.Text = temp.TenNhanVien;
                    if (temp.GioiTinh == "False")
                    {
                        rdNu.Checked = true;
                    }
                    else
                    {
                        rdNam.Checked = true;
                    }
                    if (temp.HinhAnh == null)
                    {
                        return;
                    }
                    else
                    {
                        //string folder = "D:\\Anhhhh";
                        //string pathstring = System.IO.Path.Combine(folder, temp.HinhAnh);
                        string pathstring = temp.HinhAnh;
                        txtLinkAnh.Text = pathstring;
                        picAnh.Image = null;
                        picAnh.Image = Image.FromFile(pathstring);
                    }
                }
            }
        }

    }
}
