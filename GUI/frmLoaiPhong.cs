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
    public partial class frmLoaiPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiPhong()
        {
            InitializeComponent();
        }
        LOAIPHONG _lp;
        bool _them;
        int _idlp;
        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            _lp = new LOAIPHONG();
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
            txtDongia.Enabled = t;
            txtNguoi.Enabled = t;
            txtGiuong.Enabled = t;
            chkDisable.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            txtDongia.Text = "";
            txtNguoi.Text = "";
            txtGiuong.Text = "";
            chkDisable.Checked = false;
        }
        private Boolean checkLoiNhapLieu()
        {
            Boolean kq = true;
            string strErrors = "";
            
            if (txtTen.Text.Trim() == "")
            {
                strErrors += "Chưa nhập tên loại phòng; ";
                kq = false;
                txtTen.Focus();
            }
            if (txtDongia.Text.Trim() == "")
            {
                strErrors += "Chưa nhập đơn giá; ";
                kq = false;
                txtDongia.Focus();
            }
            if (txtNguoi.Text.Trim() == "")
            {
                strErrors += "Chưa nhập số người; ";
                kq = false;
                txtNguoi.Focus();
            }
            if (txtGiuong.Text.Trim() == "")
            {
                strErrors += "Chưa nhập số giường; ";
                kq = false;
                txtGiuong.Focus();
            }
            if (!kq) MessageBox.Show(strErrors);
            return kq;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _lp.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;

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
                _lp.delete(_idlp);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                if (checkLoiNhapLieu())
                {
                    tb_LoaiPhong lp = new tb_LoaiPhong();
                    lp.TENLOAIPHONG = txtTen.Text;
                    lp.DONGIA =float.Parse(txtDongia.Text);
                    lp.SONGUOI =int.Parse(txtNguoi.Text);
                    lp.SOGIUONG =int.Parse(txtGiuong.Text);
                    lp.DISABLED = chkDisable.Checked;
                    _lp.add(lp);
                    _them = false;
                    loadData();
                    _enabled(false);
                    showHideControl(true);
                }
                

            }
            else
            {
                tb_LoaiPhong lp = _lp.getItem(_idlp);
                lp.TENLOAIPHONG = txtTen.Text;
                lp.DONGIA = float.Parse(txtDongia.Text);
                lp.SONGUOI = int.Parse(txtNguoi.Text);
                lp.SOGIUONG = int.Parse(txtGiuong.Text);
                lp.DISABLED = chkDisable.Checked;
                _lp.update(lp);
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
                _idlp = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENLOAIPHONG").ToString();
                txtDongia.Text =gvDanhSach.GetFocusedRowCellValue("DONGIA").ToString();
                txtNguoi.Text = gvDanhSach.GetFocusedRowCellValue("SONGUOI").ToString();
                txtGiuong.Text = gvDanhSach.GetFocusedRowCellValue("SOGIUONG").ToString();
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