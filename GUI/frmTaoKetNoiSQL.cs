using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace GUI
{
    public partial class frmTaoKetNoiSQL : DevExpress.XtraEditors.XtraForm
    {
        public frmTaoKetNoiSQL()
        {
            InitializeComponent();
        }
        SqlConnection getCon(string server,string databse,string user,string pass)
        {
            if (rdChungThucSQL.Checked)
            {
                return new SqlConnection("Data Source="+txtServer.Text +"; Initial Catalog="+txtDatabase.Text+";User ID="+txtUsername.Text+";Password="+txtPassword.Text+";") ;
            }
            else
            {
                return new SqlConnection("Data Source=" + txtServer.Text + "; Initial Catalog=" + txtDatabase.Text + "; Integrated Security=true;");
            }
        }
        private void frmTaoKetNoiSQL_Load(object sender, EventArgs e)
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            SqlConnection conn = getCon(txtServer.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Text);
            try
            {
                conn.Open();
                MessageBox.Show("Kết nối thành công");
            }
            catch
            {
                MessageBox.Show("Kết nối không thành công");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string enCryptServ = Encryptor.Encrypt(txtServer.Text, "qwertyuiop", true);
            string enCryptPass = Encryptor.Encrypt(txtPassword.Text, "qwertyuiop", true);
            string enCryptData = Encryptor.Encrypt(txtDatabase.Text, "qwertyuiop", true);
            string enCryptUser = Encryptor.Encrypt(txtUsername.Text, "qwertyuiop", true);
            connect cn = new connect(enCryptServ, enCryptUser, enCryptPass, enCryptData);
            cn.SaveFile();
            MessageBox.Show("Lưu file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rdChungThucWindows_Click(object sender, EventArgs e)
        {
            if (rdChungThucWindows.Checked)
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void rdChungThucSQL_Click(object sender, EventArgs e)
        {
            if (rdChungThucWindows.Checked)
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }
    }
}