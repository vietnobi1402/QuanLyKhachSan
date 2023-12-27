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
    public partial class frmThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frmThietBi()
        {
            InitializeComponent();
        }
        THIETBI _tb;
        bool _them;
        int _idtb;
        private void frmThietBi_Load(object sender, EventArgs e)
        {
            _tb = new THIETBI();
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
            chkDisable.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            txtDongia.Text = "";
            chkDisable.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _tb.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;

        }
        private Boolean checkLoiNhapLieu()
        {
            Boolean kq = true;
            string strErrors = "";

            if (txtTen.Text.Trim() == "")
            {
                strErrors += "Chưa nhập tên thiết bị; ";
                kq = false;
                txtTen.Focus();
            }
            if (txtDongia.Text.Trim() == "")
            {
                strErrors += "Chưa nhập đơn giá; ";
                kq = false;
                txtDongia.Focus();
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
                _tb.delete(_idtb);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                if (checkLoiNhapLieu())
                {
                    tb_ThietBi tb = new tb_ThietBi();
                    tb.TENTB = txtTen.Text;
                    tb.DONGIA = float.Parse(txtDongia.Text);
                    tb.DISABLED = chkDisable.Checked;
                    _tb.add(tb);
                    _them = false;
                    loadData();
                    _enabled(false);
                    showHideControl(true);
                }
                
            }
            else
            {
                tb_ThietBi tb = _tb.getItem(_idtb);
                tb.TENTB = txtTen.Text;
                tb.DONGIA = float.Parse(txtDongia.Text);
                tb.DISABLED = chkDisable.Checked;
                _tb.update(tb);
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
                _idtb = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDTB").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENTB").ToString();
                txtDongia.Text = gvDanhSach.GetFocusedRowCellValue("DONGIA").ToString();
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