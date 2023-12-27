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
using BUS;
using DAL;
namespace GUI
{
    public partial class frmDonVi : DevExpress.XtraEditors.XtraForm
    {
        public frmDonVi()
        {
            InitializeComponent();
        }
        DONVI _donvi;
        CONGTY _congty;
        bool _them;
        string _madvi;
        private void frmDonVi_Load(object sender, EventArgs e)
        {
            _donvi = new DONVI();
            _congty = new CONGTY();
            loadCongTy();
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
            cboCty.SelectedIndexChanged += CboCty_SelectedIndexChanged;
            loadDviByCty();
        }

        private void CboCty_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDviByCty();
        }

        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
            btnIn.Visible = t;
        }
        void _enabled(bool t)
        {
            txtMa.Enabled = t;
            txtTen.Enabled = t;
            txtDienThoai.Enabled = t;
            txtFax.Enabled = t;
            txtDiaChi.Enabled = t;
            txtEmail.Enabled = t;
            
        }
        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
        }
        void loadCongTy()
        {
            cboCty.DataSource = _congty.getAll();
            cboCty.DisplayMember = "TENCTY";
            cboCty.ValueMember = "MACTY";
        }
        void loadData()
        {
            gcDanhSach.DataSource = _donvi.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        void loadDviByCty()
        {
            gcDanhSach.DataSource = _donvi.getAll(cboCty.SelectedValue.ToString());
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private Boolean checkLoiNhapLieu()
        {
            Boolean kq = true;
            string strErrors = "";
            if (txtMa.Text.Trim() == "")
            {
                strErrors += "Chưa nhập mã đơn vị; ";
                kq = false;
                txtMa.Focus();
            }
            else if (txtTen.Text.Trim() == "")
            {
                strErrors += "Chưa nhập tên đơn vị;";
                kq = false;
                txtTen.Focus();
            }
            else if (txtDienThoai.Text.Trim() == "")
            {
                strErrors += "Chưa nhập số điện thoại; ";
                kq = false;
                txtDienThoai.Focus();
            }
            else if (txtFax.Text.Trim() == "")
            {
                strErrors += "Chưa nhập fax; ";
                kq = false;
                txtFax.Focus();
            }
            else if (txtEmail.Text.Trim() == "")
            {
                strErrors += "Chưa nhập email; ";
                kq = false;
                txtEmail.Focus();
            }
            else if (txtDiaChi.Text.Trim() == "")
            {
                strErrors += "Chưa nhập địa chỉ.";
                kq = false;
                txtDiaChi.Focus();
            }
            if (!kq) MessageBox.Show(strErrors);
            return kq;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
            txtMa.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            txtMa.Enabled = false;
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _donvi.delete(_madvi);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                if (checkLoiNhapLieu())
                {
                    tb_DonVi dvi = new tb_DonVi();
                    dvi.MACTY = cboCty.SelectedValue.ToString();
                    dvi.MADVI = txtMa.Text;
                    dvi.TENDVI = txtTen.Text;
                    dvi.DIACHI = txtDiaChi.Text;
                    dvi.DIENTHOAI = txtDienThoai.Text;
                    dvi.EMAIL = txtEmail.Text;
                    dvi.FAX = txtFax.Text;
                    _donvi.add(dvi);
                    _them = false;
                    loadData();
                    showHideControl(true);
                    _enabled(false);
                }
                    
            }
            else
            {
                tb_DonVi dvi = _donvi.getItem(_madvi);
                dvi.MACTY = cboCty.SelectedValue.ToString();
                dvi.TENDVI = txtTen.Text;
                dvi.DIACHI = txtDiaChi.Text;
                dvi.DIENTHOAI = txtDienThoai.Text;
                dvi.EMAIL = txtEmail.Text;
                dvi.FAX = txtFax.Text;
                _donvi.update(dvi);
                _them = false;
                loadData();
                _enabled(false);
                showHideControl(true);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _madvi = gvDanhSach.GetFocusedRowCellValue("MADVI").ToString();
                cboCty.SelectedValue = gvDanhSach.GetFocusedRowCellValue("MACTY");
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MADVI").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENDVI").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("DIENTHOAI").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("FAX").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();            
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            frmInDvi FRM = new frmInDvi();
            FRM.ShowDialog();
        }
    }
}