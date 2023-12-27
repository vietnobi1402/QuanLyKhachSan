using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        
        public frmDangNhap()
        {
            InitializeComponent();
        }
        private int dem = 3;
        private void btnDN_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "admin" && txtMatKhau.Text == "123456")
            {
                frmMain main = new frmMain();
                main.ShowDialog();
                this.Close();
            }
            else
            {
                txtTaiKhoan.Focus();
                txtTaiKhoan.SelectAll();
                txtMatKhau.Text = "";
                dem--;         
            }
            if (txtTaiKhoan.Text.Trim() == "") { MessageBox.Show("Vui lòng nhập tên tài khoản!");return; }
            if (txtMatKhau.Text.Trim() == "") { MessageBox.Show("Vui lòng nhập mật khẩu!"); return; }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}