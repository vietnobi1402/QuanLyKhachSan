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
using DAL;
using BUS;

namespace GUI
{
    public partial class frmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        KHACHHANG _kh;
        bool _them;
        int _idkh;
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            _kh = new KHACHHANG();
            loadData();
            showHideControl(true);
            _enabled(false);
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }
        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            txtDienThoai.Enabled = t;
            txtCCCD.Enabled = t;
            txtDiaChi.Enabled = t;
            txtEmail.Enabled = t;
            chkDisable.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtCCCD.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            chkDisable.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _kh.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;

        }
        private Boolean checkLoiNhapLieu()
        {
            Boolean kq = true;
            string strErrors = "";

            if (txtTen.Text.Trim() == "")
            {
                strErrors += "Chưa nhập tên khach hang; ";
                kq = false;
                txtTen.Focus();
            }
            if (txtCCCD.Text.Trim() == "")
            {
                strErrors += "Chưa nhập căn cước công dân; ";
                kq = false;
                txtCCCD.Focus();
            }
            if (txtDienThoai.Text.Trim() == "")
            {
                strErrors += "Chưa nhập điện thoại; ";
                kq = false;
                txtDienThoai.Focus();
            }
            if (txtEmail.Text.Trim() == "")
            {
                strErrors += "Chưa nhập email; ";
                kq = false;
                txtEmail.Focus();
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                strErrors += "Chưa nhập địa chỉ; ";
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
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _kh.delete(_idkh);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                if (checkLoiNhapLieu())
                {   
                    tb_KhachHang kh = new tb_KhachHang();
                    kh.HOTEN = txtTen.Text;
                    kh.DIACHI = txtDiaChi.Text;
                    kh.DIENTHOAI = txtDienThoai.Text;
                    kh.EMAIL = txtEmail.Text;
                    kh.CCCD = txtCCCD.Text;
                    kh.DISABLED = chkDisable.Checked;
                    _kh.add(kh);
                    _them = false;
                    loadData();
                    _enabled(false);
                    showHideControl(true);
                }
                
            }
            else
            {
                tb_KhachHang kh = _kh.getItem(_idkh);
                kh.HOTEN = txtTen.Text;
                kh.DIACHI = txtDiaChi.Text;
                kh.DIENTHOAI = txtDienThoai.Text;
                kh.EMAIL = txtEmail.Text;
                kh.CCCD = txtCCCD.Text;
                kh.DISABLED = chkDisable.Checked;
                _kh.update(kh);
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
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idkh =int.Parse(gvDanhSach.GetFocusedRowCellValue("IDKH").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("HOTEN").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("DIENTHOAI").ToString();
                txtCCCD.Text = gvDanhSach.GetFocusedRowCellValue("CCCD").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                chkDisable.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources._9004715_cross_delete_remove_cancel_icon;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }
    }
}