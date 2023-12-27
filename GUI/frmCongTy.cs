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
    public partial class frmCongTy : DevExpress.XtraEditors.XtraForm
    {
        public frmCongTy()
        {
            InitializeComponent();
        }
        CONGTY _congty;
        bool _them;
        string _macty;
        private void frmCongTy_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            loadData();
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
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
        private Boolean checkLoiNhapLieu()
        {
            Boolean kq = true;
            string strErrors = "";
            if (txtMa.Text.Trim() == "")
            {
                strErrors += "Chưa nhập mã công ty; ";
                kq = false;
                txtMa.Focus();
            }
            else if (txtTen.Text.Trim() == "")
            {
                strErrors += "Chưa nhập tên công ty;";
                kq = false;
                txtTen.Focus();
            }
            else if (txtDienThoai.Text.Trim() == "")
            {
                strErrors += "Chưa nhập số điện thoại;";
                kq = false;
                txtDienThoai.Focus();
            }
            else if (txtFax.Text.Trim() == "")
            {
                strErrors += "Chưa nhập fax;";
                kq = false;
                txtFax.Focus();
            }
            else if (txtEmail.Text.Trim() == "")
            {
                strErrors += "Chưa nhập email;";
                kq = false;
                txtEmail.Focus();
            }
            else if (txtDiaChi.Text.Trim() == "")
            {
                strErrors += "Chưa nhập địa chỉ";
                kq = false;
                txtDiaChi.Focus();
            }
           
            if (!kq) MessageBox.Show(strErrors);
            return kq;
        }
        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text ="" ;
            txtDienThoai.Text="" ;
            txtFax.Text="" ;
            txtEmail.Text = "";
            txtDiaChi.Text ="" ;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _congty.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            txtMa.Enabled = true;
            showHideControl(false);
            _enabled(true);
            _reset();
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
            if(MessageBox.Show("Bạn có chắc chắn xoá không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                _congty.delete(_macty);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                
                if (checkLoiNhapLieu()) {
                tb_CongTy cty = new tb_CongTy();             
                cty.MACTY = txtMa.Text;
                cty.TENCTY = txtTen.Text;
                cty.DIACHI = txtDiaChi.Text;
                cty.DIENTHOAI = txtDienThoai.Text;
                cty.EMAIL = txtEmail.Text;
                cty.FAX = txtFax.Text;
                _congty.add(cty);
                _them = false;
                loadData();
                showHideControl(true);
                _enabled(false);
                }              
                              
            }
            else
            {
                tb_CongTy cty = _congty.getItem(_macty);
                cty.TENCTY = txtTen.Text;
                cty.DIACHI = txtDiaChi.Text;
                cty.DIENTHOAI = txtDienThoai.Text;
                cty.EMAIL = txtEmail.Text;
                cty.FAX = txtFax.Text;
                _congty.update(cty);
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _macty = gvDanhSach.GetFocusedRowCellValue("MACTY").ToString();
                txtMa.Text=gvDanhSach.GetFocusedRowCellValue("MACTY").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENCTY").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("DIENTHOAI").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("FAX").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            frmIn FRM = new frmIn();
            FRM.ShowDialog();
        }

        //private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
        //    {
        //        Image img = Properties.Resources._9004715_cross_delete_remove_cancel_icon;
        //        e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
        //        e.Handled = true;
        //    }
        //}
    }
}